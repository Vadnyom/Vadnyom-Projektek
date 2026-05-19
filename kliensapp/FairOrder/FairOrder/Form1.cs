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
        private readonly string _kiemeltKategoriaId = "92be5c1c-cd74-4948-bf4e-f03016a4d9b9";

        private List<Product> _osszesTermek = new List<Product>();
        private List<KosarTetel> _kosar = new List<KosarTetel>();
        private List<System.IO.MemoryStream> _kepStreamek = new List<System.IO.MemoryStream>();
        private FlowLayoutPanel _kivalasztottKartya = null;
        private List<Product> _kiemeltTermekek = new List<Product>();

        private string _bejelentkezettEmail = "vadnyom1@gmail.com";
        private string _bejelentkezettUserId = "1";
        private string _bejelentkezettVezeteknev = "Felhasználó";
        private string _bejelentkezettKeresztnev = "Teszt";

        private int _kiemeltKartyakSzama = 6;

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

                var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products?key={_apiKey}";
                var json = await _client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<HotcakesProductResponse>(json);

                _osszesTermek = result?.Content?.Products ?? new List<Product>();


                await ToltsdBeKiemeltKartyak();
                //var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/products/forcategory/{_kiemeltKategoriaId}?key={_apiKey}";
                //var json = await _client.GetStringAsync(url);
                //var result = JsonConvert.DeserializeObject<HotcakesProductResponse>(json);
                //_kiemeltTermekek = result?.Content?.Products ?? new List<Product>();
                //await ToltsdBeKiemeltKartyak();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
            }
        }


        //private void FrissitdAListat(string szuroSku)
        //{
        //    var szurt = string.IsNullOrWhiteSpace(szuroSku)
        //        ? _osszesTermek
        //        : _osszesTermek
        //            .Where(p => p.Sku.Contains(szuroSku, StringComparison.OrdinalIgnoreCase))
        //            .ToList();

        //    FilteredSku.DataSource = null;
        //    FilteredSku.DataSource = szurt;
        //    FilteredSku.DisplayMember = "Sku";
        //}

        private readonly FilterService _filterService = new FilterService(); //ez egy �j class, amibe �tker�lt a FrissitdAListat egy r�sze (�s �n unit testet csak erre a classra fogok �rni
        private void FrissitdAListat(string szuroSku)
        {

            // eredeti code:
            //var szurt = string.IsNullOrWhiteSpace(szuroSku)
            //    ? _osszesTermek
            //    : _osszesTermek
            //        .Where(p => p.Sku.Contains(szuroSku, StringComparison.OrdinalIgnoreCase))
            //        .ToList();

            //�j: a szurt mostm�r a FilterService classt h�vja meg (oda ker�lt az eredeti sz�r�s)
            var szurt = _filterService.FilterBySku(_osszesTermek, szuroSku);

            //ez pedig v�ltozatlan
            //FilteredSku.DataSource = null;
            //FilteredSku.DataSource = szurt;
            //FilteredSku.DisplayMember = "Sku";
        }
        //>>>>>>> 6ce0ba6f16838452f57c4756f70a68792c9cdaa7


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SkuSearch_TextChanged(object sender, EventArgs e)
        {
            //FrissitdAListat(SkuSearch.Text);
            //ToltsdBeKepesLista(SkuSearch.Text);

            string szuro = SkuSearch.Text.ToLower();

            foreach (Control card in ProductImagesPanel.Controls)
            {
                if (card.Tag is Product termek)
                {
                    card.Visible = string.IsNullOrWhiteSpace(szuro) ||
                                   termek.Sku.ToLower().Contains(szuro);
                }
            }
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

                var bruttoOsszeg = _kosar.Sum(k => k.SitePrice * k.Mennyiseg);
                decimal nettoOsszeg = bruttoOsszeg / 1.27m;
                decimal afa = bruttoOsszeg - nettoOsszeg;

                var orderRequest = new OrderRequest
                {
                    UserEmail = _bejelentkezettEmail,
                    UserID = _bejelentkezettUserId,
                    IsPlaced = true,

                    TotalGrand = bruttoOsszeg,
                    TotalOrderBeforeDiscounts = nettoOsszeg,
                    ItemsTax = afa,
                    TotalTax = afa,
                    StatusCode = "09D7305D-BD95-48d2-A025-16ADC827582A",
                    OrderNumber = nextOrderNumber,
                    BillingAddress = new BillingAddress
                    {
                        FirstName = _bejelentkezettKeresztnev,
                        LastName = _bejelentkezettVezeteknev,
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
                       $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/orders?key={_apiKey}",
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
            _bejelentkezettEmail = "vadnyom1@gmail.com";
            _bejelentkezettVezeteknev = "Felhasználó";
            _bejelentkezettKeresztnev = "Teszt";
            _bejelentkezettUserId = "1";
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

            if (_kivalasztottKartya?.Tag is not Product kivalasztott)
            {
                MessageBox.Show("Válassz ki egy terméket!");
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

        private async Task ToltsdBeKepesLista(string szuro = "")
        {
            ProductImagesPanel.Controls.Clear();
            _kepStreamek.ForEach(ms => ms.Dispose());
            _kepStreamek.Clear();

            var szurtTermekek = string.IsNullOrWhiteSpace(szuro)
                ? _osszesTermek
                : _osszesTermek
                    .Where(p => p.Sku.Contains(szuro, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            foreach (var termek in szurtTermekek)
            {
                var kepUrl = $"{_baseUrl}/Portals/0/Hotcakes/Data/products/{termek.Bvin}/small/{termek.Sku}.jpg";

                var kep = new PictureBox
                {
                    Width = 100,
                    Height = 100,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };

                try
                {
                    var kepBytes = await _client.GetByteArrayAsync(kepUrl);
                    var ms = new System.IO.MemoryStream(kepBytes);
                    _kepStreamek.Add(ms);
                    kep.Image = Image.FromStream(ms);
                }
                catch
                {
                    kep.BackColor = Color.LightGray;
                }

                var sku = new Label
                {
                    Text = termek.Sku,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = 100,
                    Height = 20,
                    AutoSize = false
                };

                var ar = new Label
                {
                    Text = $"{termek.SitePrice:N0} Ft",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = 100,
                    Height = 20,
                    AutoSize = false
                };

                var card = new FlowLayoutPanel
                {
                    Width = 120,
                    Height = 165,
                    FlowDirection = FlowDirection.TopDown,
                    Cursor = Cursors.Hand,
                    Tag = termek,
                    BackColor = Color.White,
                    Padding = new Padding(5)
                };

                card.Click += (s, e) => KartyaKivalasztva(card);
                kep.Click += (s, e) => KartyaKivalasztva(card);
                sku.Click += (s, e) => KartyaKivalasztva(card);
                ar.Click += (s, e) => KartyaKivalasztva(card);

                card.Controls.Add(kep);
                card.Controls.Add(sku);
                card.Controls.Add(ar);
                ProductImagesPanel.Controls.Add(card);
            }
        }


        private void KartyaKivalasztva(FlowLayoutPanel kartya)
        {
            // Előző kijelölés törlése
            if (_kivalasztottKartya != null)
                _kivalasztottKartya.BackColor = SystemColors.Control;

            // Új kijelölés
            kartya.BackColor = Color.FromArgb(69, 91, 60);
            _kivalasztottKartya = kartya;

            // Számláló visszaállítása
            QuantityText.Text = "1";
        }

        private void KiemeltBeallButton_Click(object sender, EventArgs e)
        {
            //var valaszto = new KiemeltTermekek(_osszesTermek);
            //if (valaszto.ShowDialog() == DialogResult.OK)
            //{
            //    _kiemeltTermekek = valaszto.KivalasztottTermekek;
            //    //await 
            //    ToltsdBeKiemeltKartyak();
            //}

            var valaszto = new KiemeltTermekek(_osszesTermek);
            if (valaszto.ShowDialog() == DialogResult.OK)
            {
                _kiemeltTermekek = valaszto.KivalasztottTermekek;
                _kiemeltKartyakSzama = valaszto.KivalasztottKartyakSzama;
                //await 
                ToltsdBeKiemeltKartyak();
            }
        }

        private async Task ToltsdBeKiemeltKartyak()
        {
            ProductImagesPanel.Controls.Clear();
            _kepStreamek.ForEach(ms => ms.Dispose());
            _kepStreamek.Clear();

            for (int i = 0; i < _kiemeltKartyakSzama; i++)
            {
                if (i < _kiemeltTermekek.Count)
                {
                    var termek = _kiemeltTermekek[i];
                    var kepUrl = $"{_baseUrl}/Portals/0/Hotcakes/Data/products/{termek.Bvin}/small/{termek.Sku}.jpg";

                    var kep = new PictureBox
                    {
                        Width = 100,
                        Height = 100,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = new Padding(5),
                        Cursor = Cursors.Hand
                    };

                    try
                    {
                        var kepBytes = await _client.GetByteArrayAsync(kepUrl);
                        var ms = new System.IO.MemoryStream(kepBytes);
                        _kepStreamek.Add(ms);
                        kep.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                        kep.BackColor = Color.LightGray;
                    }

                    var sku = new Label
                    {
                        Text = termek.Sku,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = 100,
                        Height = 20,
                        AutoSize = false
                    };

                    var ar = new Label
                    {
                        Text = $"{termek.SitePrice:N0} Ft",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = 100,
                        Height = 20,
                        AutoSize = false
                    };

                    var card = new FlowLayoutPanel
                    {
                        Width = 120,
                        Height = 165,
                        FlowDirection = FlowDirection.TopDown,
                        Cursor = Cursors.Hand,
                        Tag = termek,
                        BackColor = Color.White,
                        Padding = new Padding(5)
                    };

                    card.Click += (s, e) => KartyaKivalasztva(card);
                    kep.Click += (s, e) => KartyaKivalasztva(card);
                    sku.Click += (s, e) => KartyaKivalasztva(card);
                    ar.Click += (s, e) => KartyaKivalasztva(card);

                    card.Controls.Add(kep);
                    card.Controls.Add(sku);
                    card.Controls.Add(ar);
                    ProductImagesPanel.Controls.Add(card);
                }
                else
                {
                    var uresKartya = new FlowLayoutPanel
                    {
                        Width = 110,
                        Height = 165,
                        FlowDirection = FlowDirection.TopDown,
                        BackColor = Color.WhiteSmoke,
                        Padding = new Padding(5)
                    };

                    var plusIkon = new Label
                    {
                        Text = "+",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = 100,
                        Height = 100,
                        Font = new Font("Arial", 36),
                        ForeColor = Color.LightGray
                    };

                    uresKartya.Controls.Add(plusIkon);
                    ProductImagesPanel.Controls.Add(uresKartya);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void User_Click(object sender, EventArgs e)
        {
            var form = new Bejelentkezes(_client, _baseUrl, _apiKey);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _bejelentkezettEmail = form.BejelentkezettEmail;
                _bejelentkezettVezeteknev = form.BejelentkezettVezeteknev;
                _bejelentkezettKeresztnev = form.BejelentkezettKeresztnev;
                _bejelentkezettUserId = form.BejelentkezettUserId;

                if (!string.IsNullOrEmpty(form.BejelentkezettVezeteknev))
                {
                    MessageBox.Show($"Bejelentkezve: {_bejelentkezettEmail}");
                }
            }
        }
    }
}
