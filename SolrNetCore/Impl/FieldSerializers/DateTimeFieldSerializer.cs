
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Serializes datetime fields
    /// </summary>
    public class DateTimeFieldSerializer : AbstractFieldSerializer<DateTime> {
        public static readonly string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'";

        public static string SerializeDate(DateTime dt) {
            return dt.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override IEnumerable<PropertyNode> Parse(DateTime obj) {
            yield return new PropertyNode {
                FieldValue = SerializeDate(obj),
            };
        }
    }
}