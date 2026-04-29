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
    public partial class KiemeltTermekek : Form
    {
        public List<Product> KivalasztottTermekek { get; private set; } = new List<Product>();
        private List<Product> _osszesTermek;
        private List<Product> _kivalasztottak = new List<Product>();

        public int KivalasztottKartyakSzama => (int)numericUpDown1.Value;

        public KiemeltTermekek(List<Product> osszesTermek)
        {
            InitializeComponent();
            _osszesTermek = osszesTermek;
            FrissitdAListat("");
            KiemeltFilter.TextChanged += (s, e) => FrissitdAListat(KiemeltFilter.Text);
        }

        private void KiemeltTermekek_Load(object sender, EventArgs e)
        {

        }

        private void FrissitdAListat(string szuro)
        {
            var szurt = string.IsNullOrWhiteSpace(szuro)
                ? _osszesTermek
                : _osszesTermek
                    .Where(p => p.Sku.Contains(szuro, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            KiemeltListBox.DataSource = null;
            KiemeltListBox.DataSource = szurt;
            KiemeltListBox.DisplayMember = "Sku";
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
           
            KivalasztottTermekek = _kivalasztottak;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KiemeltListBox.SelectedItem is not Product kivalasztott) return;

            if (_kivalasztottak.Count >= KivalasztottKartyakSzama)
            {
                MessageBox.Show($"Maximum {KivalasztottKartyakSzama} terméket lehet kiválasztani!");
                return;
            }

            if (_kivalasztottak.Any(k => k.Bvin == kivalasztott.Bvin))
            {
                MessageBox.Show("Ez a termék már szerepel a listában!");
                return;
            }

            _kivalasztottak.Add(kivalasztott);
            FrissitdKivalasztottak();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedItems.SelectedItem is not Product torlendo) return;
            _kivalasztottak.Remove(torlendo);
            FrissitdKivalasztottak();
        }

        private void FrissitdKivalasztottak()
        {
            SelectedItems.DataSource = null;
            SelectedItems.DataSource = _kivalasztottak.ToList();
            SelectedItems.DisplayMember = "Sku";
        }
    }
}
