using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairOrder
{
    public class FilterService
    {
        public List<Product> FilterBySku(List<Product> products, string szuroSku)
        {
            if (string.IsNullOrWhiteSpace(szuroSku))
                return products;

            return products
                .Where(p => p.Sku.Contains(szuroSku, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
