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

namespace Vadnyom.Dnn.Dnn_Vadnyom_HelloWorld.Controllers
{
    public class ItemController : DnnController
    {
        [HttpGet]
        public ActionResult Index(int step = 0, int? coatId = null, int? pantsId = null, int? bootId = null)
        {
            var model = BuildModel(step, coatId, pantsId, bootId);
            return View(model);
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
                    if (postedModel.SelectedCoatId.HasValue)
                    {
                        nextStep = 2;
                    }
                    else
                    {
                        nextStep = 1;
                        errorMessage = "Kérlek, válassz ki egy kabátot a továbblépéshez.";
                    }
                }
                else if (postedModel.CurrentStep == 2)
                {
                    if (postedModel.SelectedPantsId.HasValue)
                    {
                        nextStep = 3;
                    }
                    else
                    {
                        nextStep = 2;
                        errorMessage = "Kérlek, válassz ki egy nadrágot a továbblépéshez.";
                    }
                }
                else if (postedModel.CurrentStep == 3)
                {
                    if (postedModel.SelectedBootId.HasValue)
                    {
                        nextStep = 4;
                    }
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


        private BundleViewModel BuildModel(int step, int? coatId, int? pantsId, int? bootId)
        {

            var items = GetMockItems();

            var model = new BundleViewModel
            {
                CurrentStep = step,
                SelectedCoatId = coatId,
                SelectedPantsId = pantsId,
                SelectedBootId = bootId,

                Coats = items.Where(x => x.Category == "Coat" && x.IsActive)
                                         .OrderBy(x => x.SortOrder)
                                         .ToList(),

                Pants = items.Where(x => x.Category == "Pants" && x.IsActive)
                                         .OrderBy(x => x.SortOrder)
                                         .ToList(),

                Boots = items.Where(x => x.Category == "Boots" && x.IsActive)
                                         .OrderBy(x => x.SortOrder)
                                         .ToList(),

                SelectedCoat = items.FirstOrDefault(x => x.ItemId == coatId),
                SelectedPants = items.FirstOrDefault(x => x.ItemId == pantsId),
                SelectedBoot = items.FirstOrDefault(x => x.ItemId == bootId)
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

        private List<Item> GetMockItems()
        {
            return new List<Item>
            {
                new Item
                {
                    ItemId = 1,
                    ItemName = "Abisko vadász mellény",
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
                },
                new Item
                {
                    ItemId = 4,
                    ItemName = "Pinewood vadásznadrág",
                    ItemDescription = "Vadásznadrág",
                    Category = "Pants",
                    Price = 45900,
                    ImageUrl = "/Portals/0/Images/vadasznadrag-1.jpg",
                    SortOrder = 1,
                    IsActive = true
                },
                new Item
                {
                    ItemId = 5,
                    ItemName = "Abisko túranadrág",
                    ItemDescription = "Túranadrág",
                    Category = "Pants",
                    Price = 52900,
                    ImageUrl = "/Portals/0/Images/vadasznadrag-2.jpg",
                    SortOrder = 2,
                    IsActive = true
                },
                new Item
                {
                    ItemId = 6,
                    ItemName = "Vízálló vadásznadrág",
                    ItemDescription = "Vízálló nadrág",
                    Category = "Pants",
                    Price = 59900,
                    ImageUrl = "/Portals/0/Images/vadasznadrag-3.jpg",
                    SortOrder = 3,
                    IsActive = true
                },
                new Item
                {
                    ItemId = 7,
                    ItemName = "Vadász bakancs",
                    ItemDescription = "Magas szárú bakancs",
                    Category = "Boots",
                    Price = 69900,
                    ImageUrl = "/Portals/0/Images/bakancs-1.jpg",
                    SortOrder = 1,
                    IsActive = true
                },
                new Item
                {
                    ItemId = 8,
                    ItemName = "Téli vadász bakancs",
                    ItemDescription = "Téli bakancs",
                    Category = "Boots",
                    Price = 79900,
                    ImageUrl = "/Portals/0/Images/bakancs-2.jpg",
                    SortOrder = 2,
                    IsActive = true
                },
                new Item
                {
                    ItemId = 9,
                    ItemName = "Vízálló túrabakancs",
                    ItemDescription = "Vízálló bakancs",
                    Category = "Boots",
                    Price = 84900,
                    ImageUrl = "/Portals/0/Images/bakancs-3.jpg",
                    SortOrder = 3,
                    IsActive = true
                }
            };
        }
    }
}

 