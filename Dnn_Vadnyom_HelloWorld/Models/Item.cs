using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models
{
    [TableName("Dnn_Vadnyom_HelloWorld_Items")]
    [PrimaryKey("ItemId", AutoIncrement = true)]
    [Cacheable("Items", CacheItemPriority.Default, 20)]
    [Scope("ModuleId")]
    public class Item
    {

      
        public int ItemId { get; set; } = -1;
       
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public int AssignedUserId { get; set; }

        public int ModuleId { get; set; }

        public int CreatedByUserId { get; set; } = -1;

        public int LastModifiedByUserId { get; set; } = -1;

        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

        public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;


        // ÚJ MEZŐK A BUNDLE / TERMÉK DIZÁJNHOZ

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;


        public string ExternalProductId { get; set; }   // Hotcakes Bvin
        public string Sku { get; set; }
    }
}
