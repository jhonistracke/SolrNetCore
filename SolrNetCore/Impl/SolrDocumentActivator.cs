
using System;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Creates a new instance of document type <typeparamref name="T"/> using <see cref="Activator"/>
    /// </summary>
    /// <typeparam name="T">document type</typeparam>
    public class SolrDocumentActivator<T> : ISolrDocumentActivator<T> {
        public T Create() {
            return Activator.CreateInstance<T>();
        }
    }
}