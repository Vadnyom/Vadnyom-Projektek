using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairOrder
{
    //class OrderRequest
    //{
    //    public string ProductId { get; set; }
    //    public int Quantity { get; set; }
    //    //public string CustomerEmail { get; set; }

    //}
    public class OrderRequest
    {
        public string UserEmail { get; set; }
        public string UserID { get; set; }
        public bool IsPlaced { get; set; } = true;
        public BillingAddress BillingAddress { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class BillingAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; } = "Hungary";
    }
}
