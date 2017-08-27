
namespace SolrNetCore.Impl {
    public interface ISelfSerializingQuery : ISolrQuery {
        string Query { get; }
    }
}