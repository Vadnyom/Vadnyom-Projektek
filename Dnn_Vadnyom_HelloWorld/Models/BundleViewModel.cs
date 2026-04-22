using System.Collections.Generic;

namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models
{
    public class BundleViewModel
    {
        public int CurrentStep { get; set; }

        public IEnumerable<Item> Coats { get; set; }
        public IEnumerable<Item> Pants { get; set; }
        public IEnumerable<Item> Boots { get; set; }

        public int? SelectedCoatId { get; set; }
        public int? SelectedPantsId { get; set; }
        public int? SelectedBootId { get; set; }

        public decimal OriginalTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedTotal { get; set; }

        public bool HasCompleteBundle
        {
            get
            {
                return SelectedCoatId.HasValue
                    && SelectedPantsId.HasValue
                    && SelectedBootId.HasValue;
            }
        }
    }
}