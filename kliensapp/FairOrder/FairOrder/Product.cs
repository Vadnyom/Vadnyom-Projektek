using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairOrder
{
    public class Product
    {
        public string Bvin { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal SitePrice { get; set; }
        public bool IsAvailableForSale { get; set; }

    }
    //public class Product
    //{
    //    public string Sku { get; set; }
    //    public string ProductName { get; set; }
    //}
    //

    public class HotcakesProductResponse
    {
        public HotcakesContent Content { get; set; }
    }

    public class HotcakesContent
    {
        public List<Product> Products { get; set; }
    }

    public class KosarTetel
    {
        public string Bvin { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal SitePrice { get; set; }
        public int Mennyiseg { get; set; }
        public decimal Osszesen => SitePrice * Mennyiseg;

        public override string ToString()
        {
            return $"{Sku} x{Mennyiseg} — {Osszesen:N0} Ft";
        }
    }
}
