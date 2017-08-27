
using System.Collections;
using System.Collections.Generic;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Highlighted snippets by field
    /// </summary>
    public class HighlightedSnippets : IDictionary<string, ICollection<string>> {
        /// <summary>
        /// Highlighted snippets by field
        /// </summary>
        public IDictionary<string, ICollection<string>> Snippets {
            get { return fields; }
        }

        private readonly IDictionary<string, ICollection<string>> fields = new Dictionary<string, ICollection<string>>();

        public IEnumerator<KeyValuePair<string, ICollection<string>>> GetEnumerator() {
            return fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, ICollection<string>> item) {
            fields.Add(item);
        }

        public void Clear() {
            fields.Clear();
        }

        public bool Contains(KeyValuePair<string, ICollection<string>> item) {
            return fields.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, ICollection<string>>[] array, int arrayIndex) {
            fields.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, ICollection<string>> item) {
            return fields.Remove(item);
        }

        public int Count {
            get { return fields.Count; }
        }

        public bool IsReadOnly {
            get { return fields.IsReadOnly; }
        }

        public bool ContainsKey(string key) {
            return fields.ContainsKey(key);
        }

        public void Add(string key, ICollection<string> value) {
            fields.Add(key, value);
        }

        public bool Remove(string key) {
            return fields.Remove(key);
        }

        public bool TryGetValue(string key, out ICollection<string> value) {
            return fields.TryGetValue(key, out value);
        }

        public ICollection<string> this[string key] {
            get { return fields[key]; }
            set { fields[key] = value; }
        }

        public ICollection<string> Keys {
            get { return fields.Keys; }
        }

        public ICollection<ICollection<string>> Values {
            get { return fields.Values; }
        }
    }
}