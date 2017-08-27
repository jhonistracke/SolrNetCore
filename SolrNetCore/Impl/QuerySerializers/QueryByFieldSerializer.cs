
using System;
using System.Text.RegularExpressions;

namespace SolrNetCore.Impl.QuerySerializers
{
    public class QueryByFieldSerializer : SingleTypeQuerySerializer<SolrQueryByField>
    {
        public override string Serialize(SolrQueryByField q)
        {
            if (q.FieldName == null || q.FieldValue == null)
            {
                return null;
            }

            return q.Quoted ? string.Format("{0}:({1})", EscapeSpaces(q.FieldName), Quote(q.FieldValue)) : string.Format("{0}:({1})", q.FieldName, q.FieldValue);
        }

        public static readonly Regex SpecialCharactersRx = new Regex("(\\+|\\-|\\&\\&|\\|\\||\\!|\\{|\\}|\\[|\\]|\\^|\\(|\\)|\\\"|\\~|\\:|\\;|\\\\|\\?|\\*|\\/)", RegexOptions.Compiled);

        public static string EscapeSpaces(string value) {
            return value.Replace(" ", @"\ ");
        }

        public static string Quote(string value)
        {
            string r = SpecialCharactersRx.Replace(value, "\\$1");
            if (r.IndexOf(' ') != -1 || r == "")
                r = string.Format("\"{0}\"", r);
            return r;
        }
    }
}