using SolrNetCore.Exceptions;
using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SolrNetCore.Schema
{
    /// <summary>
    /// Parses a Solr schema xml document into a strongly typed
    /// <see cref="SolrSchema"/> object.
    /// </summary>
    public class SolrSchemaParser : ISolrSchemaParser
    {
        /// <summary>
        /// Parses the specified Solr schema xml.
        /// </summary>
        /// <param name="solrSchemaXml">The Solr schema xml to parse.</param>
        /// <returns>A strongly styped representation of the Solr schema xml.</returns>
        public SolrSchema Parse(XDocument solrSchemaXml)
        {
            var result = new SolrSchema();

            var schemaElem = solrSchemaXml.Element("schema");

            foreach (var fieldNode in schemaElem.XPathSelectElements("types/fieldType|types/fieldtype"))
            {
                var field = new SolrFieldType(fieldNode.Attribute("name").Value, fieldNode.Attribute("class").Value);
                result.SolrFieldTypes.Add(field);
            }

            var fieldsElem = schemaElem.Element("fields");

            foreach (var fieldNode in fieldsElem.Elements("field"))
            {
                var fieldTypeName = fieldNode.Attribute("type").Value;
                var fieldType = result.FindSolrFieldTypeByName(fieldTypeName);
                if (fieldType == null)
                    throw new SolrNetException(string.Format("Field type '{0}' not found", fieldTypeName));
                var field = new SolrField(fieldNode.Attribute("name").Value, fieldType);
                field.IsRequired = fieldNode.Attribute("required") != null ? fieldNode.Attribute("required").Value.ToLower().Equals(Boolean.TrueString.ToLower()) : false;
                field.IsMultiValued = fieldNode.Attribute("multiValued") != null ? fieldNode.Attribute("multiValued").Value.ToLower().Equals(Boolean.TrueString.ToLower()) : false;
                field.IsStored = fieldNode.Attribute("stored") != null ? fieldNode.Attribute("stored").Value.ToLower().Equals(Boolean.TrueString.ToLower()) : false;
                field.IsIndexed = fieldNode.Attribute("indexed") != null ? fieldNode.Attribute("indexed").Value.ToLower().Equals(Boolean.TrueString.ToLower()) : false;
                field.IsDocValues = fieldNode.Attribute("docValues") != null ? fieldNode.Attribute("docValues").Value.ToLower().Equals(Boolean.TrueString.ToLower()) : false;

                result.SolrFields.Add(field);
            }

            foreach (var dynamicFieldNode in fieldsElem.Elements("dynamicField"))
            {
                var dynamicField = new SolrDynamicField(dynamicFieldNode.Attribute("name").Value);
                result.SolrDynamicFields.Add(dynamicField);
            }

            foreach (var copyFieldNode in schemaElem.Elements("copyField"))
            {
                var copyField = new SolrCopyField(copyFieldNode.Attribute("source").Value, copyFieldNode.Attribute("dest").Value);
                result.SolrCopyFields.Add(copyField);
            }

            var uniqueKeyNode = schemaElem.Element("uniqueKey");
            if (uniqueKeyNode != null && !string.IsNullOrEmpty(uniqueKeyNode.Value))
            {
                result.UniqueKey = uniqueKeyNode.Value;
            }

            return result;
        }
    }
}