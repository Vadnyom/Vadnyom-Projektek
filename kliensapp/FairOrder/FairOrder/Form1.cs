using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace FairOrder
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl = "http://20.123.45.215";
        private readonly string _apiKey = "1-ce7c4f35-5308-40ef-a54f-74ec9333e365";

        private List<Product> _osszesTermek = new List<Product>();
        private List<KosarTetel> _kosar = new List<KosarTetel>();

        public Form1()
        {
            InitializeComponent();
            QuantityText.Text = "1";
            Load += async (s, e) => await ToltsdBeTermekeit();
        }
        
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

                await ToltsdBeKepesLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
            }
        }

        private readonly FilterService _filterService = new FilterService(); //ez egy új class, amibe átkerült a FrissitdAListat egy része (és én unit testet csak erre a classra fogok írni
        private void FrissitdAListat(string szuroSku)
        {

            // eredeti code:
            //var szurt = string.IsNullOrWhiteSpace(szuroSku)
            //    ? _osszesTermek
            //    : _osszesTermek
            //        .Where(p => p.Sku.Contains(szuroSku, StringComparison.OrdinalIgnoreCase))
            //        .ToList();

            //új: a szurt mostmár a FilterService classt hívja meg (oda került az eredeti szûrés)
            var szurt = _filterService.FilterBySku(_osszesTermek, szuroSku);

            //ez pedig változatlan
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

        private async void Order_Click(object sender, EventArgs e)
        {
         
            if (_kosar.Count == 0)
            {
                MessageBox.Show("A kosár üres.");
                return;
            }

             OrderButton.Enabled = false;

            await Task.Delay(1000);

            var allOrdersJson = await _client.GetStringAsync(
        $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/orders?key={_apiKey}");
            var allOrders = JsonConvert.DeserializeObject<dynamic>(allOrdersJson);

            int maxOrderNumber = 0;
            foreach (var order in allOrders.Content)
            {
                if (int.TryParse((string)order.OrderNumber, out int num))
                    if (num > maxOrderNumber)
                        maxOrderNumber = num;
            }
            string nextOrderNumber = (maxOrderNumber + 1).ToString();

            try
            {
                //var vegosszeg = _kosar.Sum(k => k.SitePrice * k.Mennyiseg);
                var bruttoOsszeg = _kosar.Sum(k => k.SitePrice * k.Mennyiseg);
                decimal nettoOsszeg = bruttoOsszeg / 1.27m;
                decimal afa = bruttoOsszeg - nettoOsszeg;
                // 1. Rendelés létrehozása
                var orderRequest = new OrderRequest
                {
                    UserEmail = "vadnyom1@gmail.com",
                    UserID = "1",
                    IsPlaced = true,
                    //TotalGrand = vegosszeg,
                    TotalGrand = bruttoOsszeg,
                    TotalOrderBeforeDiscounts = nettoOsszeg,
                    ItemsTax = afa,
                    TotalTax = afa,
                    StatusCode = "09D7305D-BD95-48d2-A025-16ADC827582A",
                    OrderNumber = nextOrderNumber,
                    BillingAddress = new BillingAddress
                    {
                        FirstName = "Teszt",
                        LastName = "Felhasználó",
                        CountryName = "Hungary"
                    },

                    Items = _kosar.Select(k => new OrderItem
                    {
                       
                        ProductId = k.Bvin,
                        Quantity = k.Mennyiseg,
                        BasePricePerItem = k.SitePrice,
                        AdjustedPricePerItem = k.SitePrice,
                        LineTotal = k.SitePrice * k.Mennyiseg,
                        TaxRate = 0.27m,
                        TaxPortion = (k.SitePrice * k.Mennyiseg) - (k.SitePrice * k.Mennyiseg / 1.27m),
                        ProductName = k.ProductName,
                        ProductSku = k.Sku,
                        TaxSchedule = 2,
                        ShipFromMode = 1

                    }).ToList()
                };

                var orderJson = JsonConvert.SerializeObject(orderRequest);
                var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");

               
                var orderResponse = await _client.PostAsync(
                       $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/orders?key={_apiKey}&recalculateOrder=true",
                        orderContent);

                orderResponse.EnsureSuccessStatusCode();

                var orderResponseJson = await orderResponse.Content.ReadAsStringAsync();
                var orderResult = JsonConvert.DeserializeObject<dynamic>(orderResponseJson);
                string bvin = orderResult.Content.bvin;

                var updateRequest = new
                {
                    Bvin = bvin,
                    OrderNumber = nextOrderNumber
                };
                var updateJson = JsonConvert.SerializeObject(updateRequest);
                var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
                await _client.PostAsync(
                    $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/orders/{bvin}?key={_apiKey}",
                    updateContent);


                MessageBox.Show("Rendelés sikeresen leadva!");
                _kosar.Clear();
                FrissitdAKosarat();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
            }
            finally
            {
                OrderButton.Enabled = true;
            }
            _kosar.Clear();
            FrissitdAKosarat();
        }
        

        private void Add_Click(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityText.Text, out int ertek))
                QuantityText.Text = (ertek + 1).ToString();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityText.Text, out int ertek) && ertek > 1)
                QuantityText.Text = (ertek - 1).ToString();
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            if (FilteredSku.SelectedItem is not Product kivalasztott)
            {
                MessageBox.Show("Válassz ki egy terméket a listából.");
                return;
            }

            if (!int.TryParse(QuantityText.Text, out int mennyiseg) || mennyiseg < 1)
            {
                MessageBox.Show("Érvénytelen mennyiség.");
                return;
            }
            var meglevo = _kosar.FirstOrDefault(k => k.Bvin == kivalasztott.Bvin);
            if (meglevo != null)
            {
                meglevo.Mennyiseg += mennyiseg;
            }
            else
            {
                _kosar.Add(new KosarTetel
                {
                    Bvin = kivalasztott.Bvin,
                    Sku = kivalasztott.Sku,
                    ProductName = kivalasztott.ProductName,
                    SitePrice = kivalasztott.SitePrice,
                    Mennyiseg = mennyiseg
                });
            }

            FrissitdAKosarat();
        }

        private void FrissitdAKosarat()
        {
            OrderList.DataSource = null;
            OrderList.DataSource = _kosar.ToList();
            OrderList.DisplayMember = "ToString";

            var reszosszeg = _kosar.Sum(k => k.Osszesen);
            decimal afa = reszosszeg - (reszosszeg / 1.27m);
            decimal netto = reszosszeg / 1.27m;

            PriceText.Text = $"{netto:N0} Ft";
            VATText.Text = $"{afa:N0} Ft";
            FullPriceText.Text = $"{reszosszeg:N0} Ft";
        }

        private void FilteredSku_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuantityText.Text = "1";
        }

        private void DelCart_Click(object sender, EventArgs e)
        {
            if (OrderList.SelectedItem is not KosarTetel kivalasztott)
            {
                MessageBox.Show("Válassz ki egy elemet a kosárból.");
                return;
            }

            _kosar.Remove(kivalasztott);
            FrissitdAKosarat();
        }

        private async Task ToltsdBeKepesLista()
        {
            ProductImagesPanel.Controls.Clear();

            foreach (var termek in _osszesTermek.Take(5))
            {
                var kepUrl = $"{_baseUrl}/Portals/0/Hotcakes/Data/products/{termek.Bvin}/small/{termek.Sku}.jpg";

                var kep = new PictureBox
                {
                    Width = 100,
                    Height = 100,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Margin = new Padding(5)
                };

                try
                {
                    var kepBytes = await _client.GetByteArrayAsync(kepUrl);
                    var ms = new System.IO.MemoryStream(kepBytes);
                    kep.Image = Image.FromStream(ms);
                }
                catch (Exception ex)
                {
                    kep.BackColor = Color.LightGray;
                    System.Diagnostics.Debug.WriteLine($"Kép betöltési hiba: {termek.Sku} - {ex.Message}");
                }

                var sku = new Label
                {
                    Text = termek.Sku,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = 100
                };

                var card = new FlowLayoutPanel
                {
                    Width = 110,
                    Height = 130,
                    FlowDirection = FlowDirection.TopDown
                };

                card.Controls.Add(kep);
                card.Controls.Add(sku);
                ProductImagesPanel.Controls.Add(card);
            }
        }
    }
}
