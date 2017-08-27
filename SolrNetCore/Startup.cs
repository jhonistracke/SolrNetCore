using Microsoft.Practices.ServiceLocation;
using SolrNet.Impl;
using SolrNetCore.Impl;
using SolrNetCore.Impl.DocumentPropertyVisitors;
using SolrNetCore.Impl.FacetQuerySerializers;
using SolrNetCore.Impl.FieldParsers;
using SolrNetCore.Impl.FieldSerializers;
using SolrNetCore.Impl.QuerySerializers;
using SolrNetCore.Impl.ResponseParsers;
using SolrNetCore.Mapping;
using SolrNetCore.Mapping.Validation;
using SolrNetCore.Mapping.Validation.Rules;
using SolrNetCore.Schema;
using SolrNetCore.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SolrNetCore
{
    /// <summary>
    /// SolrNetCore initialization manager
    /// </summary>
    public static class Startup
    {
        public static readonly Container Container = new Container();

        static Startup()
        {
            InitContainer();
        }

        public static void InitContainer()
        {
            ServiceLocator.SetLocatorProvider(() => Container);
            Container.Clear();
            var mapper = new MemoizingMappingManager(new AttributesMappingManager());

            Container.Register<IReadOnlyMappingManager>(c => mapper);

            var fieldParser = new DefaultFieldParser();
            Container.Register<ISolrFieldParser>(c => fieldParser);

            var fieldSerializer = new DefaultFieldSerializer();
            Container.Register<ISolrFieldSerializer>(c => fieldSerializer);

            Container.Register<ISolrQuerySerializer>(c => new DefaultQuerySerializer(c.GetInstance<ISolrFieldSerializer>()));
            Container.Register<ISolrFacetQuerySerializer>(c => new DefaultFacetQuerySerializer(c.GetInstance<ISolrQuerySerializer>(), c.GetInstance<ISolrFieldSerializer>()));

            Container.Register<ISolrDocumentPropertyVisitor>(c => new DefaultDocumentVisitor(c.GetInstance<IReadOnlyMappingManager>(), c.GetInstance<ISolrFieldParser>()));

            var solrSchemaParser = new SolrSchemaParser();
            Container.Register<ISolrSchemaParser>(c => solrSchemaParser);

            var solrDIHStatusParser = new SolrDIHStatusParser();
            Container.Register<ISolrDIHStatusParser>(c => solrDIHStatusParser);

            var headerParser = new HeaderResponseParser<string>();
            Container.Register<ISolrHeaderResponseParser>(c => headerParser);

            var extractResponseParser = new ExtractResponseParser(headerParser);
            Container.Register<ISolrExtractResponseParser>(c => extractResponseParser);

            Container.Register<IValidationRule>(typeof(MappedPropertiesIsInSolrSchemaRule).FullName, c => new MappedPropertiesIsInSolrSchemaRule());
            Container.Register<IValidationRule>(typeof(RequiredFieldsAreMappedRule).FullName, c => new RequiredFieldsAreMappedRule());
            Container.Register<IValidationRule>(typeof(UniqueKeyMatchesMappingRule).FullName, c => new UniqueKeyMatchesMappingRule());
            Container.Register<IValidationRule>(typeof(MultivaluedMappedToCollectionRule).FullName, c => new MultivaluedMappedToCollectionRule());
            Container.Register<IMappingValidator>(c => new MappingValidator(c.GetInstance<IReadOnlyMappingManager>(), c.GetAllInstances<IValidationRule>().ToArray()));

            Container.Register<ISolrStatusResponseParser>(c => new SolrStatusResponseParser());
        }

        /// <summary>
        /// Initializes SolrNetCore with the built-in container
        /// </summary>
        /// <typeparam name="T">Document type</typeparam>
        /// <param name="serverURL">Solr URL (i.e. "http://localhost:8983/solr")</param>
        public static void Init<T>(string serverURL)
        {
            var connection = new SolrConnection(serverURL)
            {
                //Cache = Container.GetInstance<ISolrCache>(),
            };

            Init<T>(connection);
        }

        /// <summary>
        /// Initializes SolrNetCore with the built-in container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        public static void Init<T>(ISolrConnection connection)
        {
            var connectionKey = string.Format("{0}.{1}.{2}", typeof(SolrConnection), typeof(T), connection.GetType());
            Container.Register(connectionKey, c => connection);

            var activator = new SolrDocumentActivator<T>();
            Container.Register<ISolrDocumentActivator<T>>(c => activator);

            Container.Register(ChooseDocumentResponseParser<T>);

            Container.Register<ISolrAbstractResponseParser<T>>(c => new DefaultResponseParser<T>(c.GetInstance<ISolrDocumentResponseParser<T>>()));

            Container.Register<ISolrMoreLikeThisHandlerQueryResultsParser<T>>(c => new SolrMoreLikeThisHandlerQueryResultsParser<T>(c.GetAllInstances<ISolrAbstractResponseParser<T>>().ToArray()));
            Container.Register<ISolrQueryExecuter<T>>(c => new SolrQueryExecuter<T>(c.GetInstance<ISolrAbstractResponseParser<T>>(), connection, c.GetInstance<ISolrQuerySerializer>(), c.GetInstance<ISolrFacetQuerySerializer>(), c.GetInstance<ISolrMoreLikeThisHandlerQueryResultsParser<T>>()));

            Container.Register(ChooseDocumentSerializer<T>);

            Container.Register<ISolrBasicOperations<T>>(c => new SolrBasicServer<T>(connection, c.GetInstance<ISolrQueryExecuter<T>>(), c.GetInstance<ISolrDocumentSerializer<T>>(), c.GetInstance<ISolrSchemaParser>(), c.GetInstance<ISolrHeaderResponseParser>(), c.GetInstance<ISolrQuerySerializer>(), c.GetInstance<ISolrDIHStatusParser>(), c.GetInstance<ISolrExtractResponseParser>()));
            Container.Register<ISolrBasicReadOnlyOperations<T>>(c => new SolrBasicServer<T>(connection, c.GetInstance<ISolrQueryExecuter<T>>(), c.GetInstance<ISolrDocumentSerializer<T>>(), c.GetInstance<ISolrSchemaParser>(), c.GetInstance<ISolrHeaderResponseParser>(), c.GetInstance<ISolrQuerySerializer>(), c.GetInstance<ISolrDIHStatusParser>(), c.GetInstance<ISolrExtractResponseParser>()));

            Container.Register<ISolrOperations<T>>(c => new SolrServer<T>(c.GetInstance<ISolrBasicOperations<T>>(), Container.GetInstance<IReadOnlyMappingManager>(), Container.GetInstance<IMappingValidator>()));
            Container.Register<ISolrReadOnlyOperations<T>>(c => new SolrServer<T>(c.GetInstance<ISolrBasicOperations<T>>(), Container.GetInstance<IReadOnlyMappingManager>(), Container.GetInstance<IMappingValidator>()));

            var coreAdminKey = typeof(ISolrCoreAdmin).Name + connectionKey;
            Container.Register<ISolrCoreAdmin>(coreAdminKey, c => new SolrCoreAdmin(connection, c.GetInstance<ISolrHeaderResponseParser>(), c.GetInstance<ISolrStatusResponseParser>()));
        }

        private static ISolrDocumentSerializer<T> ChooseDocumentSerializer<T>(IServiceLocator c)
        {
            if (typeof(T) == typeof(Dictionary<string, object>))
                return (ISolrDocumentSerializer<T>)new SolrDictionarySerializer(c.GetInstance<ISolrFieldSerializer>());
            return new SolrDocumentSerializer<T>(c.GetInstance<IReadOnlyMappingManager>(), c.GetInstance<ISolrFieldSerializer>());
        }

        private static ISolrDocumentResponseParser<T> ChooseDocumentResponseParser<T>(IServiceLocator c)
        {
            if (typeof(T) == typeof(Dictionary<string, object>))
                return (ISolrDocumentResponseParser<T>)new SolrDictionaryDocumentResponseParser(c.GetInstance<ISolrFieldParser>());
            return new SolrDocumentResponseParser<T>(c.GetInstance<IReadOnlyMappingManager>(), c.GetInstance<ISolrDocumentPropertyVisitor>(), c.GetInstance<ISolrDocumentActivator<T>>());
        }
    }
}