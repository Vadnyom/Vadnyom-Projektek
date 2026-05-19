    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Net;
    using System.IO;
    using Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models;

    namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Components
    {
        public class HotcakesApiService
        {
            private readonly string _baseUrl = "http://20.123.45.215";
            private readonly string _apiKey = "1-ce7c4f35-5308-40ef-a54f-74ec9333e365";

            public List<HotcakesProduct> GetProducts()
            {
                var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products?key={_apiKey}";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 10000;
                request.ReadWriteTimeout = 10000;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<HotcakesProductResponse>(json);
                    return result?.Content?.Products ?? new List<HotcakesProduct>();
                }
            }

            public List<Item> MapToBundleItems(List<HotcakesProduct> apiProducts)
            {
                var mapped = new List<Item>();
                int id = 1;

                foreach (var product in apiProducts)
                {
                    if (!product.IsAvailableForSale)
                        continue;

                    var category = ResolveCategory(product);

                    if (string.IsNullOrWhiteSpace(category))
                        continue;

                    var imageUrl = $"{_baseUrl}/Portals/0/Hotcakes/Data/products/{product.Bvin}/small/{product.Sku}.jpg";

                    mapped.Add(new Item
                    {
                        ItemId = id++,
                        ItemName = product.ProductName,
                        ItemDescription = product.Sku,
                        Category = category,
                        Price = product.SitePrice,
                        ImageUrl = imageUrl,
                        SortOrder = id,
                        IsActive = true,
                        ExternalProductId = product.Bvin,
                        Sku = product.Sku
                    });
                }

                return mapped;
            }

            private string ResolveCategory(HotcakesProduct product)
            {
                if (product == null || string.IsNullOrWhiteSpace(product.Sku))
                    return null;

                var sku = product.Sku.Trim().ToUpperInvariant();

                if (sku.StartsWith("JACK"))
                    return "Coat";

                if (sku.StartsWith("TROU"))
                    return "Pants";

                if (sku.StartsWith("BOOT"))
                    return "Boots";

                return null;
            }
        }
    }