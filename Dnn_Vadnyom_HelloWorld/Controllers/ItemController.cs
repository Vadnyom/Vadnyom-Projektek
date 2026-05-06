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



namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
{
    public class ItemController : DnnController
    {
        private readonly HotcakesApiService _apiService = new HotcakesApiService();

        [HttpGet]
        public ActionResult Index(int step = 0, int? coatId = null, int? pantsId = null, int? bootId = null)
        {
            var model = BuildModel(step, coatId, pantsId, bootId);
            return View(model);
        }

        public ActionResult AddBundleNext()
        {
            var step = Session["BundleStep"] as int? ?? 1;

            string sku = null;

            if (step == 1)
            {
                sku = Session["BundleSku1"] as string;
                Session["BundleStep"] = 2;
            }
            else if (step == 2)
            {
                sku = Session["BundleSku2"] as string;
                Session["BundleStep"] = 3;
            }
            else if (step == 3)
            {
                sku = Session["BundleSku3"] as string;
                Session["BundleStep"] = 4;
            }
            else
            {
                Session.Remove("BundleSku1");
                Session.Remove("BundleSku2");
                Session.Remove("BundleSku3");
                Session.Remove("BundleStep");

                return Redirect("/Felhasznalo-Bejelentkezes/Cart");
            }

            return Redirect("/Felhasznalo-Bejelentkezes/Cart?QuickAddSku="
                + Server.UrlEncode(sku)
                + "&QuickAddQty=1");
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

            var cartUrl = "/Felhasznalo-Bejelentkezes/Cart";

            var coatUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedCoat.Sku) + "&QuickAddQty=1";
            var pantsUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedPants.Sku) + "&QuickAddQty=1";
            var bootUrl = cartUrl + "?QuickAddSku=" + Server.UrlEncode(model.SelectedBoot.Sku) + "&QuickAddQty=1";

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
                '{coatUrl}',
                '{pantsUrl}',
                '{bootUrl}'
            ];

            for (const url of urls) {{
                await fetch(url + '&_=' + new Date().getTime(), {{
                    method: 'GET',
                    credentials: 'same-origin'
                }});
            }}

            window.location.href = '{cartUrl}';
        }}

        addBundleItems();
    </script>
</body>
</html>";

            return Content(html, "text/html");
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