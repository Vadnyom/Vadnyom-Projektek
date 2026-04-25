using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
//using ReaLTaiizor.Controls;
//using ReaLTaiizor.Enum.Poison;
//using ReaLTaiizor.Manager;

namespace FairOrder
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl = "http://20.123.45.215";
        private readonly string _apiKey = "1-ce7c4f35-5308-40ef-a54f-74ec9333e365";

        private List<Product> _osszesTermek = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            Load += async (s, e) => await ToltsdBeTermekeit();
        }
        //private async Task ToltsdBeTermekeit()
        //{
        //    try
        //    {
        //        var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products?key={_apiKey}";
        //        var json = await _client.GetStringAsync(url);
        //        var result = JsonConvert.DeserializeObject<HotcakesProductResponse>(json);
        //        _osszesTermek = result.Products ?? new List<Product>();

        //        FrissitdAListat(SkuSearch.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Hiba: {ex.Message}");
        //    }
        //}
        private async Task ToltsdBeTermekeit()
        {
            try
            {
                MessageBox.Show("Betöltés elkezdõdött");

                var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products?key={_apiKey}";

                MessageBox.Show($"URL: {url}");

                var json = await _client.GetStringAsync(url);

                MessageBox.Show($"Válasz hossza: {json.Length} karakter");

                var result = JsonConvert.DeserializeObject<HotcakesProductResponse>(json);


                MessageBox.Show($"Termékek száma: {result?.Content?.Products?.Count ?? 0}");

                _osszesTermek = result?.Content?.Products ?? new List<Product>();

                FrissitdAListat(SkuSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
            }
        }
        
        private void FrissitdAListat(string szuroSku)
        {
            var szurt = string.IsNullOrWhiteSpace(szuroSku)
                ? _osszesTermek
                : _osszesTermek
                    .Where(p => p.Sku.Contains(szuroSku, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            FilteredSku.DataSource = null;
            FilteredSku.DataSource = szurt;
            FilteredSku.DisplayMember = "Sku";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SkuSearch_TextChanged(object sender, EventArgs e)
        {
            FrissitdAListat(SkuSearch.Text);
        }
    }
}
