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
        public decimal Price { get; set; }
        public bool IsAvailableForSale { get; set; }

    }
    //public class Product
    //{
    //    public string Sku { get; set; }
    //    public string ProductName { get; set; }
    //}

    public class HotcakesProductResponse
    {
        public HotcakesContent Content { get; set; }
    }

    public class HotcakesContent
    {
        public List<Product> Products { get; set; }
    }
}
