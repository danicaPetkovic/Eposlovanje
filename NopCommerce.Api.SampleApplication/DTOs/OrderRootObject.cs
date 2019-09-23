using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerce.Api.SampleApplication.DTOs
{
    public class OrderRootObject
    {
        [JsonProperty("orders")]
        public List<OrderApi> Orders { get; set; }
    }
}