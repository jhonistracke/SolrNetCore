
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses 1-dimensional fields
    /// </summary>
    public class CollectionFieldParser : ISolrFieldParser {
        private readonly ISolrFieldParser valueParser;

        public CollectionFieldParser(ISolrFieldParser valueParser) {
            this.valueParser = valueParser;
        }

        public bool CanHandleSolrType(string solrType) {
            return solrType == "arr";
        }

        public bool CanHandleType(Type t) {
            return t != typeof (string) &&
                   typeof (IEnumerable).IsAssignableFrom(t) &&
                   !typeof (IDictionary).IsAssignableFrom(t) &&
                   !TypeHelper.IsGenericAssignableFrom(typeof (IDictionary<,>), t);
        }

        public object Parse(XElement field, Type t) {
            var genericTypes = t.GetGenericArguments();
            if (genericTypes.Length == 1) {
                // ICollection<int>, etc
                return GetGenericCollectionProperty(field, genericTypes);
            }
            if (t.IsArray) {
                // int[], string[], etc
                return GetArrayProperty(field, t);
            }
            if (t.IsInterface) {
                // ICollection
                return GetNonGenericCollectionProperty(field);
            }
            return null;
        }

        public IList GetNonGenericCollectionProperty(XElement field) {
            var l = new ArrayList();
            foreach (var arrayValueNode in field.Elements()) {
                l.Add(valueParser.Parse(arrayValueNode, typeof(object)));
            }
            return l;
        }


        public Array GetArrayProperty(XElement field, Type t) {
            // int[], string[], etc
            var arr = (Array)Activator.CreateInstance(t, new object[] { field.Elements().Count() });
            var arrType = Type.GetType(t.ToString().Replace("[]", ""));
            int i = 0;
            foreach (var arrayValueNode in field.Elements()) {
                arr.SetValue(valueParser.Parse(arrayValueNode, arrType), i);
                i++;
            }
            return arr;
        }


        public IList GetGenericCollectionProperty(XElement field, Type[] genericTypes) {
            // ICollection<int>, etc
            var gt = genericTypes[0];
            var l = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(gt));
            foreach (var arrayValueNode in field.Elements()) {
                l.Add(valueParser.Parse(arrayValueNode, gt));
            }
            return l;
        }
    }
}