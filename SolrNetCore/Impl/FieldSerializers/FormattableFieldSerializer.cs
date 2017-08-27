
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Serializes objects that implement <see cref="IFormattable"/>
    /// </summary>
    public class FormattableFieldSerializer : ISolrFieldSerializer {
        public bool CanHandleType(Type t) {
            return typeof (IFormattable).IsAssignableFrom(t);
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            if (obj == null)
                yield break;
            var v = (IFormattable) obj;
            yield return new PropertyNode {
                FieldValue = v.ToString(null, CultureInfo.InvariantCulture)
            };
        }
    }
}