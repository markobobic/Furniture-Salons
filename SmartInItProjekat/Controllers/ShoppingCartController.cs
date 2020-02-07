using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartInItProjekat.Infrastructure;
using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using SmartInItProjekat.ViewModels;

namespace SmartInItProjekat.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class ShoppingCartController : Controller
    {

        IUsers _dbUsers;
        IFurnitureRepo _db;
        IBillRepo _dbBill;
        
        public ShoppingCartController(IUsers dbUsers,IFurnitureRepo db,IBillRepo dbBill)
        {
            _db = db;
            _dbUsers = dbUsers;
            _dbBill = dbBill;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new ShoppingCartViewModel
            {
                ShoppingCart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public ActionResult AddToCart(int? furnitureId)
        {
            Furniture furniture = _db.GetById(furnitureId);
            GetCart().AddItem(furniture, 1);
            return RedirectToAction("Index", "Furnitures", new { area = "" });
           
        }
       
        public ActionResult AddOneMore(int furnitureId)
        {
            Furniture furniture = _db.GetById(furnitureId);
            if (furniture.Amount > 0)
            {
                GetCart().AddItem(furniture, 1);
                
                return PartialView("GetShoppingCart");
            }
            return View(furniture);
        }

        public ActionResult DecreaseByOne(int furnitureId)
        {
            Furniture furniture = _db.GetById(furnitureId);
            if (furniture.Amount > 0)
            {
                var cart = GetCart();
                GetCart().DecreaseQuantity(furniture);
                return PartialView("GetShoppingCart", cart);
            }
            return View(furniture);
        }

        public RedirectToRouteResult RemoveFromCart(int furnitureId, string returnUrl)
        {
            Furniture furniture = _db.GetById(furnitureId);
            if (furniture != null)
            {
                GetCart().RemoveItem(furniture);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private ShoppingCart GetCart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult GetShoppingCart(string returnUrl)
        {
            return PartialView(new ShoppingCartViewModel
            {
                ShoppingCart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public PartialViewResult EditQuantity(string returnUrl)
        {
            return PartialView(new ShoppingCartViewModel
            {
                ShoppingCart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public PartialViewResult Summary(ShoppingCart cart)
        {
            return PartialView(cart);
        }

        public ActionResult GenereteBill(ShoppingCart cart)
        {

            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Cart is empty!");
            }
            if (ModelState.IsValid)
            {
                BillViewModel bill = new BillViewModel();
                foreach (var item in cart.Items.ToList())
                {
                    Furniture furniture = _db.GetById(item.Furniture.FurnitureId);
                    BillItem billItem = new BillItem
                    {
                        Furniture = item.Furniture.Name,
                        FurnitureCategory = item.Furniture.Category.Name,
                        Amount = item.Quantity
                    };
                    if (furniture.Amount - billItem.Amount < 0)
                    {
                        var exceded = billItem.Amount - furniture.Amount;
                        ModelState.AddModelError(string.Empty, $"An item under the name {furniture.Name} is currently out of reach.\nPlease remove {exceded} units of {furniture.Name} from your ");
                        return View("OutOfLimit");

                    }
                    billItem.FurnitureShop = furniture.FurnitureSalon.Name;
                    billItem.Price = item.Furniture.Price * item.Quantity;
                    bill.BillItems.Add(billItem);

                }
                bill.Tax = Tax.RegularTax;
                var subTotal = bill.BillItems.Aggregate(new decimal { }, (result, item) => { result += item.Price; return result; });
                bill.TimeOfPurchase = DateTime.Now;
                bill.TotalPrice = subTotal + (subTotal * bill.Tax);
                string firstName = _dbUsers.GetAllBuyers().Where(x => x.UserName.Equals(User.Identity.Name)).Select(x => x.FirstName).First();
                string lastName = _dbUsers.GetAllBuyers().Where(x => x.UserName.Equals(User.Identity.Name)).Select(x => x.LastName).First();
                bill.Buyer = firstName + " " + lastName;
                return View("../Bills/Bill", bill);
            }
            return Index("ShoppingCart");
        }


        [HttpPost]
        public ActionResult Purchase(ShoppingCart cart,int quantity,string returnUrl)
        {
            
            if (cart.ViewItems.Count() == 0)
            {
                ModelState.AddModelError("", "Cart is empty!");
            }
            if (ModelState.IsValid)
            {
                foreach(var item in cart.ViewItems.ToList())
                {
                    Furniture furniture = _db.GetById(item.Furniture.FurnitureId);
                    item.Quantity=quantity; 
                    GetCart().AddItemWithPurchase(furniture, item.Quantity);
                   
                }
                
                if (Request.Form["submit"] != null)
                {
                    return RedirectToAction("Index", new { returnUrl });
                }
                if (Request.Form["save"] != null)
                {
                    return PartialView("Loading");
                }

            }
            return Index("ShoppingCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompletePurchase(ShoppingCart cart)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Cart is empty!");
            }

            if (ModelState.IsValid)
            {

                Bill bill = new Bill();

                foreach (var item in cart.Items)
                {
                    BillItem billItem = new BillItem
                    {
                        Furniture = item.Furniture.Name,
                        FurnitureCategory = item.Furniture.Category.Name,
                        Amount = item.Quantity,
                        Price = item.Furniture.Price * item.Quantity,
                        FurnitureShop = item.Furniture.FurnitureSalon.Name
                    };
                    Furniture furniture = _db.GetById(item.Furniture.FurnitureId);
                    furniture.Amount = furniture.Amount - billItem.Amount;
                    _db.Update(furniture);
                    foreach(var k in cart.ViewItems)
                    {
                        k.Furniture.Amount = furniture.Amount;
                    }
                    bill.BillItems.Add(billItem);

                }

                bill.Tax = Tax.RegularTax;
                var subTotal = bill.BillItems.Aggregate(new decimal { }, (result, item) => { result += item.Price; return result; });
                bill.TotalPrice = subTotal + (subTotal * bill.Tax);
                bill.TimeOfPurchase = DateTime.Now;
                string firstName = _dbUsers.GetAll().Where(x => x.UserName.Equals(User.Identity.Name)).Select(x => x.FirstName).First();
                string lastName = _dbUsers.GetAll().Where(x => x.UserName.Equals(User.Identity.Name)).Select(x => x.LastName).First();
                bill.Buyer = firstName + " " + lastName;
                _dbBill.Add(bill);
                cart.Clear();
                return View("OrderSubmited");
            }
            return RedirectToRoute(new { controller = "ShoppingCart", action = "Index" });
        }
    }
}