
using System.Collections.Generic;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Parses bool values
    /// </summary>
    public class BoolFieldSerializer : AbstractFieldSerializer<bool> {
        public string SerializeBool(bool o) {
            return o.ToString().ToLowerInvariant();
        }

        public override IEnumerable<PropertyNode> Parse(bool obj) {
            yield return new PropertyNode {
                FieldValue = SerializeBool(obj),
            };
        }
    }
}