using System;
using System.Linq;
using System.Web.Mvc;
using Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Components;
using Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotcakes.Commerce.Extensions;
using Hotcakes.Commerce;
using Hotcakes.Commerce.Orders;
using Hotcakes.Commerce.Dnn;
using Hotcakes.Commerce.Catalog;
using System.Web;
using Hotcakes.Commerce.Marketing;



namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
{
    public class ItemController : DnnController
    {
        private readonly HotcakesApiService _apiService = new HotcakesApiService();

        [HttpGet]
        public ActionResult DebugCartLineTotals()
        {
            try
            {
                var hccApp = Hotcakes.Commerce.HotcakesApplication.Current;
                var cart = hccApp.OrderServices.CurrentShoppingCart();

                if (cart == null)
                {
                    return Content("cart == null", "text/plain");
                }

                if (cart.Items == null)
                {
                    return Content("cart.Items == null", "text/plain");
                }

                if (cart.Items.Count == 0)
                {
                    return Content("A kosár üres.", "text/plain");
                }

                var lines = new List<string>();

                lines.Add("DEBUG CART LINE TOTALS");
                lines.Add("Kosársorok száma: " + cart.Items.Count);
                lines.Add("");

                foreach (var item in cart.Items)
                {
                    lines.Add("SKU: " + item.ProductSku);
                    lines.Add("Name: " + item.ProductName);
                    lines.Add("BasePricePerItem: " + item.BasePricePerItem);
                    lines.Add("AdjustedPricePerItem: " + item.AdjustedPricePerItem);
                    lines.Add("Quantity: " + item.Quantity);
                    lines.Add("LineTotal: " + item.LineTotal);
                    lines.Add("LineTotalWithDiscounts: " + item.LineTotalWithDiscounts);
                    lines.Add("LineTotalWithoutDiscounts: " + item.LineTotalWithoutDiscounts);
                    lines.Add("TotalDiscounts(): " + item.TotalDiscounts());

                    if (item.DiscountDetails != null)
                    {
                        lines.Add("DiscountDetails.Count: " + item.DiscountDetails.Count);

                        foreach (var d in item.DiscountDetails)
                        {
                            lines.Add("Discount Description: " + d.Description);
                            lines.Add("Discount Amount: " + d.Amount);
                            lines.Add("Discount Type: " + d.DiscountType);
                            lines.Add("PromotionId: " + d.PromotionId);
                            lines.Add("ActionId: " + d.ActionId);
                        }
                    }
                    else
                    {
                        lines.Add("DiscountDetails: null");
                    }

                    lines.Add("-------------------------");
                }

                return Content(string.Join("\r\n", lines), "text/plain");
            }
            catch (Exception ex)
            {
                return Content("DebugCartLineTotals hiba: " + ex.ToString(), "text/plain");
            }
        }

        public ActionResult Index(int step = 0, int? coatId = null, int? pantsId = null, int? bootId = null)
        {
            var model = BuildModel(step, coatId, pantsId, bootId);

            if (TempData["BundleErrorMessage"] != null)
            {
                model.ErrorMessage = TempData["BundleErrorMessage"].ToString();
            }

            return View(model);
        }

        public ActionResult FinalizeBundle()
        {
            var cartUrl = "/Kosar";

            try
            {
                var hccApp = Hotcakes.Commerce.HotcakesApplication.Current;
                var cart = hccApp.OrderServices.CurrentShoppingCart();

                var bundleGroupId = Session["PendingBundleGroupId"] as string;
                var skuList = Session["PendingBundleSkus"] as string[];

                if (string.IsNullOrWhiteSpace(bundleGroupId) || skuList == null || skuList.Length != 3)
                {
                    return Redirect(cartUrl);
                }

                foreach (var item in cart.Items)
                {
                    var sku = GetLineItemSku(item);

                    if (string.IsNullOrWhiteSpace(sku))
                        continue;

                    if (!skuList.Contains(sku, StringComparer.OrdinalIgnoreCase))
                        continue;

                    var devId = "VadnyomBundleModule";

                    item.CustomPropertySet(devId, "IsBundleItem", "true");
                    item.CustomPropertySet(devId, "BundleGroupId", bundleGroupId);
                    item.CustomPropertySet(devId, "BundleDiscountPercent", "15");
                    item.CustomPropertySet(devId, "BundleSource", "VadnyomBundleModule");

                    TryPrefixBundleName(item);
                    ApplyBundleLineDiscount(item, 0.15m);
                }


                hccApp.OrderServices.Orders.Update(cart, true);


                Session.Remove("PendingBundleGroupId");
                Session.Remove("PendingBundleSkus");

                return Redirect(cartUrl);
            }
            catch (Exception ex)
            {
                return Content("FinalizeBundle hiba: " + ex.Message);
            }
        }

        private const string BundleDevId = "VadnyomBundleModule";

        private bool IsBundleItem(object lineItem)
        {
            if (lineItem == null)
                return false;



            var method = lineItem.GetType().GetMethod("CustomPropertyGet", new[]
            {
        typeof(string),
        typeof(string)
    });

            if (method == null)
                return false;

            var value = method.Invoke(lineItem, new object[]
            {
        BundleDevId,
        "IsBundleItem"
            });

            return value != null && value.ToString().Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        private string GetBundleGroupId(object lineItem)
        {
            if (lineItem == null)
                return null;

            var method = lineItem.GetType().GetMethod("CustomPropertyGet", new[]
            {
        typeof(string),
        typeof(string)
    });

            if (method == null)
                return null;

            var value = method.Invoke(lineItem, new object[]
            {
        BundleDevId,
        "BundleGroupId"
            });

            return value == null ? null : value.ToString();
        }

        public ActionResult RemoveBundle(string groupId)
        {
            var cartUrl = "/Kosar";

            if (string.IsNullOrWhiteSpace(groupId))
            {
                return Redirect(cartUrl);
            }

            try
            {
                var hccApp = Hotcakes.Commerce.HotcakesApplication.Current;
                var cart = hccApp.OrderServices.CurrentShoppingCart();

                var itemsToRemove = cart.Items
                    .Where(x => IsBundleItem(x) && GetBundleGroupId(x) == groupId)
                    .ToList();

                foreach (var item in itemsToRemove)
                {
                    cart.Items.Remove(item);
                }

                hccApp.OrderServices.Orders.Update(cart, true);
                hccApp.CalculateOrderAndSave(cart);

                return Redirect(cartUrl);
            }
            catch (Exception ex)
            {
                return Content("RemoveBundle hiba: " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult Index(BundleViewModel postedModel, string navigation)
        {
            int nextStep = postedModel.CurrentStep;
            string errorMessage = null;

            if (navigation == "start")
            {
                nextStep = 1;
            }
            else if (navigation == "home")
            {
                nextStep = 0;
            }
            else if (navigation == "step1")
            {
                nextStep = 1;
            }
            else if (navigation == "step2")
            {
                nextStep = 2;
            }
            else if (navigation == "step3")
            {
                nextStep = 3;
            }
            else if (navigation == "clear")
            {
                if (postedModel.CurrentStep == 1)
                {
                    postedModel.SelectedCoatId = null;
                    nextStep = 1;
                }
                else if (postedModel.CurrentStep == 2)
                {
                    postedModel.SelectedPantsId = null;
                    nextStep = 2;
                }
                else if (postedModel.CurrentStep == 3)
                {
                    postedModel.SelectedBootId = null;
                    nextStep = 3;
                }
            }
            else if (navigation == "next")
            {
                if (postedModel.CurrentStep == 1)
                {
                    if (postedModel.SelectedCoatId.HasValue) nextStep = 2;
                    else
                    {
                        nextStep = 1;
                        errorMessage = "Kérlek, válassz ki egy kabátot a továbblépéshez.";
                    }
                }
                else if (postedModel.CurrentStep == 2)
                {
                    if (postedModel.SelectedPantsId.HasValue) nextStep = 3;
                    else
                    {
                        nextStep = 2;
                        errorMessage = "Kérlek, válassz ki egy nadrágot a továbblépéshez.";
                    }
                }
                else if (postedModel.CurrentStep == 3)
                {
                    if (postedModel.SelectedBootId.HasValue) nextStep = 4;
                    else
                    {
                        nextStep = 3;
                        errorMessage = "Kérlek, válassz ki egy bakancsot a továbblépéshez.";
                    }
                }
            }
            else if (navigation == "prev")
            {
                if (postedModel.CurrentStep == 4) nextStep = 3;
                else if (postedModel.CurrentStep == 3) nextStep = 2;
                else if (postedModel.CurrentStep == 2) nextStep = 1;
                else nextStep = 0;
            }

            var model = BuildModel(
                nextStep,
                postedModel.SelectedCoatId,
                postedModel.SelectedPantsId,
                postedModel.SelectedBootId
            );

            model.ErrorMessage = errorMessage;

            ViewBag.ScrollToBundle = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddToCart(BundleViewModel postedModel)
        {
            var model = BuildModel(
                4,
                postedModel.SelectedCoatId,
                postedModel.SelectedPantsId,
                postedModel.SelectedBootId
            );

            if (model.SelectedCoat == null || model.SelectedPants == null || model.SelectedBoot == null)
            {
                model.ErrorMessage = "A kosárba helyezéshez mindhárom terméket ki kell választani.";
                return View("Index", model);
            }

            var bundleGroupId = "BND-" + Guid.NewGuid().ToString("N");

            Session["PendingBundleGroupId"] = bundleGroupId;
            Session["PendingBundleSkus"] = new[]
            {
        model.SelectedCoat.Sku,
        model.SelectedPants.Sku,
        model.SelectedBoot.Sku
    };

            var cartUrl = "/Kosar";

            var coatUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedCoat.Sku) + "&QuickAddQty=1";
            var pantsUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedPants.Sku) + "&QuickAddQty=1";
            var bootUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedBoot.Sku) + "&QuickAddQty=1";

            var finalizeUrl = Url.Action("FinalizeBundle", "Item");

            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>Kosárba helyezés</title>
</head>
<body>
    <p>Termékek kosárba helyezése folyamatban...</p>

    <script>
        async function addBundleItems() {{
            const urls = [
                '{HttpUtility.JavaScriptStringEncode(coatUrl)}',
                '{HttpUtility.JavaScriptStringEncode(pantsUrl)}',
                '{HttpUtility.JavaScriptStringEncode(bootUrl)}'
            ];

            for (const url of urls) {{
                await fetch(url + '&_=' + new Date().getTime(), {{
                    method: 'GET',
                    credentials: 'same-origin'
                }});
            }}

            window.location.href = '{HttpUtility.JavaScriptStringEncode(finalizeUrl)}';
        }}

        addBundleItems();
    </script>
</body>
</html>";

            return Content(html, "text/html");
        }

        private string GetLineItemSku(object lineItem)
        {
            if (lineItem == null)
                return null;

            var type = lineItem.GetType();


            var possibleNames = new[]
            {
        "ProductSku",
        "Sku",
        "ProductSKU",
        "SKU"
    };

            foreach (var name in possibleNames)
            {
                var prop = type.GetProperty(name);

                if (prop == null)
                    continue;

                var value = prop.GetValue(lineItem, null);

                if (value != null)
                    return value.ToString();
            }

            return null;
        }

        private void ApplyBundleLineDiscount(LineItem item, decimal discountPercent)
        {
            if (item == null)
                return;

            var originalPrice = item.BasePricePerItem;

            var discountedPrice = Math.Round(
                originalPrice * (1 - discountPercent),
                0,
                MidpointRounding.AwayFromZero
            );

            item.IsUserSuppliedPrice = true;

            item.BasePricePerItem = discountedPrice;
            item.AdjustedPricePerItem = discountedPrice;
            item.LineTotal = discountedPrice * item.Quantity;

            if (item.DiscountDetails != null)
            {
                item.DiscountDetails.Clear();
            }
        }

        private void TryPrefixBundleName(object lineItem)
        {
            if (lineItem == null)
                return;

            var type = lineItem.GetType();

            var possibleNameProperties = new[]
            {
        "ProductName",
        "ProductShortDescription",
        "ProductDescription"
    };

            foreach (var name in possibleNameProperties)
            {
                var prop = type.GetProperty(name);

                if (prop == null || !prop.CanRead || !prop.CanWrite)
                    continue;

                var currentValue = prop.GetValue(lineItem, null) as string;

                if (string.IsNullOrWhiteSpace(currentValue))
                    continue;

                if (currentValue.StartsWith("Csomag:"))
                    return;

                prop.SetValue(lineItem, "Csomag: " + currentValue, null);
                return;
            }
        }

        private BundleViewModel BuildModel(int step, int? coatId, int? pantsId, int? bootId)
        {
            List<Item> items;
            string apiError = null;

            try
            {
                var apiProducts = _apiService.GetProducts();
                items = _apiService.MapToBundleItems(apiProducts);
            }
            catch (Exception ex)
            {
                items = new List<Item>();
                apiError = "API hiba: " + ex.Message;
            }

            var model = new BundleViewModel
            {
                CurrentStep = step,
                SelectedCoatId = coatId,
                SelectedPantsId = pantsId,
                SelectedBootId = bootId,

                Coats = items.Where(x => x.Category == "Coat" && x.IsActive)
             .OrderByDescending(x => x.Price)
             .Take(9)
             .ToList(),

                Pants = items.Where(x => x.Category == "Pants" && x.IsActive)
             .OrderByDescending(x => x.Price)
             .Take(9)
             .ToList(),

                Boots = items.Where(x => x.Category == "Boots" && x.IsActive)
             .OrderByDescending(x => x.Price)
             .Take(9)
             .ToList(),

                SelectedCoat = items.FirstOrDefault(x => x.ItemId == coatId),
                SelectedPants = items.FirstOrDefault(x => x.ItemId == pantsId),
                SelectedBoot = items.FirstOrDefault(x => x.ItemId == bootId),

                ErrorMessage = apiError
            };

            var selectedItems = items.Where(x =>
                x.ItemId == coatId ||
                x.ItemId == pantsId ||
                x.ItemId == bootId).ToList();

            model.OriginalTotal = selectedItems.Sum(x => x.Price);

            if (coatId.HasValue && pantsId.HasValue && bootId.HasValue)
            { 
                model.DiscountAmount = model.OriginalTotal * 0.15m;
                model.DiscountedTotal = model.OriginalTotal - model.DiscountAmount;
            }
            else
            {
                model.DiscountAmount = 0;
                model.DiscountedTotal = model.OriginalTotal;
            }

            return model;
        }

    }
}
