
using System;
using System.Collections.Generic;


namespace SolrNetCore.Impl {
    /// <summary>
    /// Serializes a value to be consumed by the Solr update handler
    /// </summary>
    public interface ISolrFieldSerializer {
        /// <summary>
        /// True if this serializer can handle the type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool CanHandleType(Type t);

        /// <summary>
        /// Serializes a value to be consumed by the Solr update handler
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>List of <see cref="PropertyNode"/>s, each represents a XML node to be passed to the Solr update handler</returns>
        IEnumerable<PropertyNode> Serialize(object obj);
    }
}