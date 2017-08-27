using SolrNetCore.Impl.FieldParsers;
using SolrNetCore.Schema;
using System;
using System.Collections.Generic;

namespace SolrNetCore.Mapping.Validation.Rules
{
    /// <summary>
    /// Represents a rule that validates that fields mapped to a solr field with mutilvalued set to true
    /// are of a type that implements <see cref="IEnumerable{T}" />.
    /// </summary>
    public class MultivaluedMappedToCollectionRule : IValidationRule
    {
        /// <summary>
        /// Validates the specified the mapped document against the solr schema.
        /// </summary>
        /// <param name="documentType">Document type</param>
        /// <param name="solrSchema">The solr schema.</param>
        /// <param name="mappingManager">The mapping manager.</param>
        /// <returns>
        /// A collection of <see cref="ValidationResult"/> objects with any issues found during validation.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(Type documentType, SolrSchema solrSchema, IReadOnlyMappingManager mappingManager)
        {
            var collectionFieldParser = new CollectionFieldParser(null); // Used to check if the type is a collection type.

            foreach (var prop in mappingManager.GetFields(documentType))
            {
                var solrField = solrSchema.FindSolrFieldByName(prop.Key);
                if (solrField == null)
                    continue;
                var isCollection = collectionFieldParser.CanHandleType(prop.Value.Property.PropertyType);
                if (solrField.IsMultiValued && !isCollection)
                    yield return new ValidationError(String.Format("SolrField '{0}' is multivalued while property '{1}.{2}' is not mapped as a collection.", solrField.Name, prop.Value.Property.DeclaringType, prop.Value.Property.Name));
                else if (!solrField.IsMultiValued && isCollection)
                    yield return new ValidationError(String.Format("SolrField '{0}' is not multivalued while property '{1}.{2}' is mapped as a collection.", solrField.Name, prop.Value.Property.DeclaringType, prop.Value.Property.Name));
            }
        }
    }
}