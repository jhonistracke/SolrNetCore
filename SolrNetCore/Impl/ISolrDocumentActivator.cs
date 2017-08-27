
namespace SolrNetCore.Impl {
    /// <summary>
    /// Instantiates a new document instance of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public interface ISolrDocumentActivator<T> {
        /// <summary>
        /// Instantiates a new document instance of type <typeparamref name="T"/>
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}