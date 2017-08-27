using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Commands
{
    /// <summary>
    /// Optimizes Solr's index
    /// </summary>
    /// <seealso href="http://wiki.apache.org/jakarta-lucene/LuceneFAQ">Lucene FAQ</seealso>
	public class OptimizeCommand : ISolrCommand
    {

        /// <summary>
        /// Block until index changes are flushed to disk
        /// Default is true
        /// </summary>
        public bool? WaitFlush { get; set; }

        /// <summary>
        /// Block until a new searcher is opened and registered as the main query searcher, making the changes visible. 
        /// Default is true
        /// </summary>
        public bool? WaitSearcher { get; set; }

        /// <summary>
        /// Merge segments with deletes away
        /// Default is false
        /// </summary>
        public bool? ExpungeDeletes { get; set; }

        /// <summary>
        /// Optimizes down to, at most, this number of segments
        /// Default is 1
        /// </summary>
        public int? MaxSegments { get; set; }

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
		public string Execute(ISolrConnection connection)
        {
            var node = new XElement("optimize");

            var ps = new[] {
                new KeyValuePair<bool?, string>(WaitSearcher, "waitSearcher"),
                new KeyValuePair<bool?, string>(WaitFlush, "waitFlush"),
                new KeyValuePair<bool?, string>(ExpungeDeletes, "expungeDeletes")
            };

            foreach (var p in ps)
            {
                if (!p.Key.HasValue) continue;

                var att = new XAttribute(p.Value, p.Key.Value.ToString().ToLower());
                node.Add(att);
            }

            if (MaxSegments.HasValue)
            {
                var att = new XAttribute("maxSegments", MaxSegments.ToString());
                node.Add(att);
            }

            return connection.Post("/update", node.ToString(SaveOptions.DisableFormatting));
        }
    }
}