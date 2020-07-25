using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NewsService.Models
{
    public class News
    {
        public string Heading { get; set; }

        public string Content { get; set; }

        public bool IsPriority { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NewsCategory Category { get; set; }
    }
}
