using Newtonsoft.Json;
using System.Collections.Generic;

namespace NopCommerce.Api.SampleApplication.DTOs
{
    public class ProductApi
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public List<ImageApi> Images { get; set; }
    }

    public class ImageApi
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("position")]
        public int position { get; set; }
        [JsonProperty("src")]
        public string src { get; set; }
    }
}