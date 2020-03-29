using System.Text.Json;
using MongoDB.Bson;

namespace estiaApi.Models
{
    public class ListRequest
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public string Range { get; set; }

        public BsonDocument FilterValue
        {
            get
            {
                return BsonDocument.Parse(Filter);
            }
        }

        public string[] SortValue
        {
            get
            {
                return JsonSerializer.Deserialize<string[]>(Sort);
            }
        }

        public int[] RangeValue
        {
            get
            {
                return JsonSerializer.Deserialize<int[]>(Range);
            }
        }
    }
}