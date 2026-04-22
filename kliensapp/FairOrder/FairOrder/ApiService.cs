//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FairOrder
//{
//    class ApiService
//    {
//    }
//}
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using HotcakesOrderClient.Models;
//using FairOrder;

namespace FairOrder
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public ApiService(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _client = new HttpClient();
            // Hotcakes API hitelesítés – az egyedi konfig alapján módosíthatod
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {1 - ce7c4f35 - 5308 - 40ef - a54f - 74ec9333e365}");
        }

        // SKU alapján termék lekérése
        public async Task<Product> GetProductBySkuAsync(string sku)
        {
            var response = await _client.GetAsync($"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products/bySku/{sku}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(json);
        }

        // Rendelés leadása
        public async Task<bool> CreateOrderAsync(OrderRequest order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/orders", content);
            return response.IsSuccessStatusCode;
        }
    }
}