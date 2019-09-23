using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerce.Api.SampleApplication.DTOs
{
    public class OrderApi
    {
         [JsonProperty("customer_currency_code")]
         public string customer_currency_code { get; set; }

        [JsonProperty("payment_method_system_name")]
        public string payment_method_system_name { get; set; }

        [JsonProperty("order_total")]
        public string order_total { get; set; }

        [JsonProperty("billing_address")]
        public Billing billing { get; set; }

        [JsonProperty("order_items")]
        public List<OrderItem> orderItem { get; set; }
    }

    public class Billing
    {
        [JsonProperty("address1")]
        public string address1 { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }
    }

    public class OrderItem
    {
        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("product")]
        public ProductApi city { get; set; }

    }
}