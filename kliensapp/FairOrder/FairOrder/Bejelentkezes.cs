using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairOrder
{
    public partial class Bejelentkezes : Form
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public string BejelentkezettEmail { get; private set; }
        public string BejelentkezettUserId { get; private set; }
        public string BejelentkezettVezeteknev { get; private set; }
        public string BejelentkezettKeresztnev { get; private set; }

        public Bejelentkezes(HttpClient client, string baseUrl, string apiKey)
        {
            InitializeComponent();
            _client = client;
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            //lblHiba.Visible = false;
        }
        //public Bejelentkezes()
        //{
        //    InitializeComponent();

        //}

        private void Bejelentkezes_Load(object sender, EventArgs e)
        {

        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            //BejelentkezettEmail = LogInEmail.Text.Trim();
            //DialogResult = DialogResult.OK;
            //Close();
            BejelentkezettEmail = LogInEmail.Text.Trim();

            try
            {
                var url = $"{_baseUrl}/DesktopModules/Hotcakes/API/rest/v1/customeraccounts/byemail/{BejelentkezettEmail}?key={_apiKey}";
                var json = await _client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<dynamic>(json);

                if (result?.Content != null)
                {
                    BejelentkezettVezeteknev = (string)result.Content.LastName ?? "";
                    BejelentkezettKeresztnev = (string)result.Content.FirstName ?? "";
                    BejelentkezettUserId = (string)result.Content.Bvin ?? "1";
                }
            }
            catch { }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
