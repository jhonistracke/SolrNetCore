namespace SolrNetCore
{
    /// <summary>
    /// Contains parameters that can be specified when adding a document to the index.
    /// </summary>
    /// <remarks>
    /// See http://wiki.apache.org/solr/UpdateXmlMessages#Optional_attributes_for_.22add.22
    /// </remarks>
    public class AddParameters : UpdateParameters
    {
        /// <summary>
        /// Gets or sets the document overwrite option.
        /// </summary>
        /// <value>If <c>true</c>, newer documents will replace previously added documents with the same uniqueKey.</value>
        public bool? Overwrite { get; set; }
    }

    /// <summary>
    /// Contains parameters than can be specified when deleting a document from the index.
    /// </summary>
    /// <remarks>
    /// CommitWithin works in SOLR 3.6+ - see https://issues.apache.org/jira/browse/SOLR-2280
    /// </remarks>
    public class DeleteParameters : UpdateParameters
    {

    }

    /// <summary>
    /// Contains parameters that can be specified when making any update to the index.
    /// </summary>
    public abstract class UpdateParameters
    {
        /// <summary>
        /// Gets or sets the time period (in milliseconds) within which the document will be committed to the index.
        /// </summary>
        /// <value>The time period (in milliseconds) within which the document will be committed to the index.</value>
        public int? CommitWithin { get; set; }
    }

}
