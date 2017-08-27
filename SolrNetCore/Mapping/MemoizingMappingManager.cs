
using System;
using System.Collections.Generic;
using SolrNetCore.Utils;

namespace SolrNetCore.Mapping {
    /// <summary>
    /// Memoizing decorator for a mapping manager
    /// </summary>
    public class MemoizingMappingManager : IReadOnlyMappingManager {
        private readonly Converter<Type, IDictionary<string,SolrFieldModel>> memoGetFields;
        private readonly Converter<Type, SolrFieldModel> memoGetUniqueKey;
        private readonly IReadOnlyMappingManager mapper;
        private ICollection<Type> registeredTypes;
        private readonly object registeredTypesLock = new object();

        public MemoizingMappingManager(IReadOnlyMappingManager mapper) {
            memoGetFields = Memoizer.Memoize<Type, IDictionary<string,SolrFieldModel>>(mapper.GetFields);
            memoGetUniqueKey = Memoizer.Memoize<Type, SolrFieldModel>(mapper.GetUniqueKey);
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets fields mapped for this type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Null if <paramref name="type"/> is not mapped</returns>
        public IDictionary<string,SolrFieldModel> GetFields(Type type) {
            return memoGetFields(type);
        }

        public SolrFieldModel GetUniqueKey(Type type) {
            return memoGetUniqueKey(type);
        }

        public ICollection<Type> GetRegisteredTypes() {
            lock (registeredTypesLock) {
                if (registeredTypes == null)
                    registeredTypes = mapper.GetRegisteredTypes();
                return new List<Type>(registeredTypes);
            }
        }
    }
}