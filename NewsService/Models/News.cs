using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NewsService.Models
{
    /// <summary>
    /// News class
    /// </summary>
    public class News
    {
        /// <summary>
        /// Heading
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Priority or normal news
        /// </summary>
        public bool IsPriority { get; set; }

        /// <summary>
        /// News category
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public NewsCategory Category { get; set; }
    }
}
