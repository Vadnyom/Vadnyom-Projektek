using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairOrder
{

    public class OrderRequest
    {
        public string UserEmail { get; set; }
        public string UserID { get; set; }
        public bool IsPlaced { get; set; } = true;
        public BillingAddress BillingAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalGrand { get; set; }
        public int StoreId { get; set; } = 1;

        public string StatusCode { get; set; }

    }

    public class OrderItem
    {
        //public string ProductId { get; set; }
        //public int Quantity { get; set; }
        //public decimal BasePricePerItem { get; set; }
        //public string ProductName { get; set; }
        //public string ProductSku { get; set; }
        //public int TaxSchedule { get; set; } = 2;
        //public int ShipFromMode { get; set; } = 1;
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal BasePricePerItem { get; set; }
        public decimal AdjustedPricePerItem { get; set; }
        public decimal LineTotal { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public int TaxSchedule { get; set; } = 2;
        public int ShipFromMode { get; set; } = 1;
        public int StoreId { get; set; } = 1;
    }

    public class BillingAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; } = "Hungary";
    }


    //public class OrderRequest
    //{
    //    public string UserEmail { get; set; }
    //    public string UserID { get; set; }
    //    public bool IsPlaced { get; set; } = true;
    //    public BillingAddress BillingAddress { get; set; }
    //}

    //public class BillingAddress
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Phone { get; set; }
    //    public string CountryName { get; set; } = "Hungary";
    //    public string CountryBvin { get; set; } = "ide_a_magyarország_bvin";
    //}

    //public class OrderItem
    //{
    //    public string ProductBvin { get; set; }
    //    public int Quantity { get; set; }
    //}

}
