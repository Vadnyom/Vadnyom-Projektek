/*
' Copyright (c) 2026 Vadnyom
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Linq;
using System.Web.Mvc;
using Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Components;
using Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;

//namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
//{
//    [DnnHandleError]
//    public class ItemController : DnnController
//    {

//        public ActionResult Delete(int itemId)
//        {
//            ItemManager.Instance.DeleteItem(itemId, ModuleContext.ModuleId);
//            return RedirectToDefaultRoute();
//        }

//        public ActionResult Edit(int itemId = -1)
//        {
//            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

//            var userlist = UserController.GetUsers(PortalSettings.PortalId);
//            var users = from user in userlist.Cast<UserInfo>().ToList()
//                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

//            ViewBag.Users = users;

//            var item = (itemId == -1)
//                 ? new Item { ModuleId = ModuleContext.ModuleId }
//                 : ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);

//            return View(item);
//        }

//        [HttpPost]
//        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
//        public ActionResult Edit(Item item)
//        {
//            if (item.ItemId == -1)
//            {
//                item.CreatedByUserId = User.UserID;
//                item.CreatedOnDate = DateTime.UtcNow;
//                item.LastModifiedByUserId = User.UserID;
//                item.LastModifiedOnDate = DateTime.UtcNow;

//                ItemManager.Instance.CreateItem(item);
//            }
//            else
//            {
//                var existingItem = ItemManager.Instance.GetItem(item.ItemId, item.ModuleId);
//                existingItem.LastModifiedByUserId = User.UserID;
//                existingItem.LastModifiedOnDate = DateTime.UtcNow;
//                existingItem.ItemName = item.ItemName;
//                existingItem.ItemDescription = item.ItemDescription;
//                existingItem.AssignedUserId = item.AssignedUserId;

//                ItemManager.Instance.UpdateItem(existingItem);
//            }

//            return RedirectToDefaultRoute();
//        }

//        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
//        public ActionResult Index()
//        {
//            var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
//            return View(items);
//        }
//    }
//}










using System.Collections.Generic;

//namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
//{
//    public class ItemController : DnnController
//    {
//        public ActionResult Index()
//        {
//            var model = BuildModel(step: 1);

//            return View(model);
//        }

//        [HttpPost]
//        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
//        public ActionResult SelectItem(BundleViewModel postedModel, int selectedItemId, string category)
//        {
//            var model = BuildModel(postedModel.CurrentStep);

//            model.SelectedCoatId = postedModel.SelectedCoatId;
//            model.SelectedPantsId = postedModel.SelectedPantsId;
//            model.SelectedBootId = postedModel.SelectedBootId;

//            if (category == "Coat")
//            {
//                model.SelectedCoatId = selectedItemId;
//                model.CurrentStep = 2;
//            }
//            else if (category == "Pants")
//            {
//                model.SelectedPantsId = selectedItemId;
//                model.CurrentStep = 3;
//            }
//            else if (category == "Boots")
//            {
//                model.SelectedBootId = selectedItemId;
//                model.CurrentStep = 4;
//            }

//            CalculateTotals(model);

//            return View("Index", model);
//        }

//        private BundleViewModel BuildModel(int step)
//        {
//            var items = GetMockItems();

//            return new BundleViewModel
//            {
//                CurrentStep = step,

//                Coats = items
//                    .Where(x => x.Category == "Coat" && x.IsActive)
//                    .OrderBy(x => x.SortOrder)
//                    .ToList(),

//                Pants = items
//                    .Where(x => x.Category == "Pants" && x.IsActive)
//                    .OrderBy(x => x.SortOrder)
//                    .ToList(),

//                Boots = items
//                    .Where(x => x.Category == "Boots" && x.IsActive)
//                    .OrderBy(x => x.SortOrder)
//                    .ToList()
//            };
//        }

//        private void CalculateTotals(BundleViewModel model)
//        {
//            var items = GetMockItems();

//            var selectedItems = items
//                .Where(x =>
//                    x.ItemId == model.SelectedCoatId ||
//                    x.ItemId == model.SelectedPantsId ||
//                    x.ItemId == model.SelectedBootId)
//                .ToList();

//            model.OriginalTotal = selectedItems.Sum(x => x.Price);

//            if (model.HasCompleteBundle)
//            {
//                model.DiscountAmount = model.OriginalTotal * 0.15m;
//                model.DiscountedTotal = model.OriginalTotal - model.DiscountAmount;
//            }
//            else
//            {
//                model.DiscountAmount = 0;
//                model.DiscountedTotal = model.OriginalTotal;
//            }
//        }

//        private List<Item> GetMockItems()
//        {
//            return new List<Item>
//            {
//                new Item
//                {
//                    ItemId = 1,
//                    ItemName = "Abisko vadász mellény",
//                    ItemDescription = "Vadász mellény",
//                    Category = "Coat",
//                    Price = 68900,
//                    ImageUrl = "/Portals/0/Images/abisko-melleny.jpg",
//                    SortOrder = 1,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 2,
//                    ItemName = "Abisko 2 vadász kabát",
//                    ItemDescription = "Vadász kabát",
//                    Category = "Coat",
//                    Price = 132900,
//                    ImageUrl = "/Portals/0/Images/abisko-kabat.jpg",
//                    SortOrder = 2,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 3,
//                    ItemName = "Abisko 2.0 női vadász kabát",
//                    ItemDescription = "Női vadász kabát",
//                    Category = "Coat",
//                    Price = 109900,
//                    ImageUrl = "/Portals/0/Images/abisko-noi-kabat.jpg",
//                    SortOrder = 3,
//                    IsActive = true
//                },

//                new Item
//                {
//                    ItemId = 4,
//                    ItemName = "Pinewood vadásznadrág",
//                    ItemDescription = "Vadásznadrág",
//                    Category = "Pants",
//                    Price = 45900,
//                    ImageUrl = "/Portals/0/Images/vadasznadrag-1.jpg",
//                    SortOrder = 1,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 5,
//                    ItemName = "Abisko túranadrág",
//                    ItemDescription = "Túranadrág",
//                    Category = "Pants",
//                    Price = 52900,
//                    ImageUrl = "/Portals/0/Images/vadasznadrag-2.jpg",
//                    SortOrder = 2,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 6,
//                    ItemName = "Vízálló vadásznadrág",
//                    ItemDescription = "Vízálló nadrág",
//                    Category = "Pants",
//                    Price = 59900,
//                    ImageUrl = "/Portals/0/Images/vadasznadrag-3.jpg",
//                    SortOrder = 3,
//                    IsActive = true
//                },

//                new Item
//                {
//                    ItemId = 7,
//                    ItemName = "Vadász bakancs",
//                    ItemDescription = "Magas szárú bakancs",
//                    Category = "Boots",
//                    Price = 69900,
//                    ImageUrl = "/Portals/0/Images/bakancs-1.jpg",
//                    SortOrder = 1,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 8,
//                    ItemName = "Téli vadász bakancs",
//                    ItemDescription = "Téli bakancs",
//                    Category = "Boots",
//                    Price = 79900,
//                    ImageUrl = "/Portals/0/Images/bakancs-2.jpg",
//                    SortOrder = 2,
//                    IsActive = true
//                },
//                new Item
//                {
//                    ItemId = 9,
//                    ItemName = "Vízálló túrabakancs",
//                    ItemDescription = "Vízálló bakancs",
//                    Category = "Boots",
//                    Price = 84900,
//                    ImageUrl = "/Portals/0/Images/bakancs-3.jpg",
//                    SortOrder = 3,
//                    IsActive = true
//                }
//            };
//        }
//    }
//}








namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
{
    public class ItemController : DnnController
    {
        public ActionResult Index()
        {
            var model = new BundleViewModel
            {
                CurrentStep = 1,
                Coats = new List<Item>
        {
            new Item
            {
                ItemId = 1,
                ItemName = "AAAAAAAAAAAAAAAAAAAAAAAAAAA",
                ItemDescription = "Vadász mellény",
                Category = "Coat",
                Price = 68900,
                ImageUrl = "/Portals/0/Images/abisko-melleny.jpg",
                SortOrder = 1,
                IsActive = true
            },
            new Item
            {
                ItemId = 2,
                ItemName = "Abisko 2 vadász kabát",
                ItemDescription = "Vadász kabát",
                Category = "Coat",
                Price = 132900,
                ImageUrl = "/Portals/0/Images/abisko-kabat.jpg",
                SortOrder = 2,
                IsActive = true
            },
            new Item
            {
                ItemId = 3,
                ItemName = "Abisko 2.0 női vadász kabát",
                ItemDescription = "Női vadász kabát",
                Category = "Coat",
                Price = 109900,
                ImageUrl = "/Portals/0/Images/abisko-noi-kabat.jpg",
                SortOrder = 3,
                IsActive = true
            }
        },
                Pants = new List<Item>(),
                Boots = new List<Item>()
            };

            return View(model);
        }
    }
}

 