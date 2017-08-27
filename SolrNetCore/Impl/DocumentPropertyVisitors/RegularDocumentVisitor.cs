using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl.DocumentPropertyVisitors
{
    /// <summary>
    /// Pass-through document visitor
    /// </summary>
    public class RegularDocumentVisitor : ISolrDocumentPropertyVisitor
    {
        private readonly ISolrFieldParser parser;
        private readonly IReadOnlyMappingManager mapper;

        /// <summary>
        /// Pass-through document visitor
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="mapper"></param>
        public RegularDocumentVisitor(ISolrFieldParser parser, IReadOnlyMappingManager mapper)
        {
            this.parser = parser;
            this.mapper = mapper;
        }

        public void Visit(object doc, string fieldName, XElement field)
        {
            var allFields = mapper.GetFields(doc.GetType());
            SolrFieldModel thisField;
            if (!allFields.TryGetValue(fieldName, out thisField))
                return;
            if (!thisField.Property.CanWrite)
                return;
            if (parser.CanHandleSolrType(field.Name.LocalName) &&
                parser.CanHandleType(thisField.Property.PropertyType))
            {
                var v = parser.Parse(field, thisField.Property.PropertyType);
                try
                {
                    thisField.Property.SetValue(doc, v, null);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException(string.Format("Could not convert value '{0}' to property '{1}' of document type {2}", v, thisField.Property.Name, thisField.Property.DeclaringType), e);
                }
            }
        }
    }
}