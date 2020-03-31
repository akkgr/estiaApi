using System.Text.Json;
using MongoDB.Bson;

namespace estiaApi.Models
{
    public class ListRequest
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public string Page { get; set; }

        public BsonDocument FilterDefinition
        {
            get
            {
                return BsonDocument.Parse(Filter);
            }
        }

        public BsonDocument SortDefinition
        {
            get
            {
                var sort = JsonSerializer.Deserialize<string[]>(Sort);
                return new BsonDocument(sort[0], sort[1] == "ASC" ? 1 : -1);
            }
        }

        public int[] PagingInfo
        {
            get
            {
                var p = JsonSerializer.Deserialize<int[]>(Page);
                return new int[] { (p[0] - 1) * p[1], p[1] };
            }
        }
    }
}