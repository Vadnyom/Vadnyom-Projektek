using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models
{
    public class HotcakesProduct
    {
        [JsonProperty("Bvin")]
        public string Bvin { get; set; }

        [JsonProperty("Sku")]
        public string Sku { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("SitePrice")]
        public decimal SitePrice { get; set; }

        [JsonProperty("IsAvailableForSale")]
        public bool IsAvailableForSale { get; set; }
    }

    public class HotcakesContent
    {
        [JsonProperty("Products")]
        public List<HotcakesProduct> Products { get; set; }
    }

    public class HotcakesProductResponse
    {
        [JsonProperty("Content")]
        public HotcakesContent Content { get; set; }
    }
}