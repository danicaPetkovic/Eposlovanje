using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerce.Api.SampleApplication.DTOs
{
    public class ProductRootObject
    {
        [JsonProperty("products")]
        public List<ProductApi> Products { get; set; }
    }
}