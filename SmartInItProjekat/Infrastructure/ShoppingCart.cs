using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartInItProjekat.Models;
using SmartInItProjekat.ViewModels;

namespace SmartInItProjekat.Infrastructure
{
    public class ShoppingCart
    {
        private List<ShoppingCarItem> items = new List<ShoppingCarItem>();
        public List<ShoppingCarItem> Viewitems = new List<ShoppingCarItem>();
        public void AddItem(Furniture furniture, int quantity)
        {
            ShoppingCarItem item = Viewitems
            .Where(p => p.Furniture.FurnitureId == furniture.FurnitureId)
            .FirstOrDefault();
            if (item == null)
            {
                Viewitems.Add(new ShoppingCarItem
                {
                    Furniture = furniture,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
            if (Viewitems.Count() == 2)
            {
                Viewitems.RemoveAt(0);
            }
        }
        public void AddItemWithPurchase(Furniture furniture, int quantity)
        {
            ShoppingCarItem item = items
            .Where(p => p.Furniture.FurnitureId == furniture.FurnitureId)
            .FirstOrDefault();
            if (item == null)
            {
                items.Add(new ShoppingCarItem
                {
                    Furniture = furniture,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity = quantity;
            }
        }

        public void RemoveItem(Furniture furniture)
        {
            items.RemoveAll(l => l.Furniture.FurnitureId == furniture.FurnitureId);
        }
        
        public bool Check(int furnitureAmount, int itemAmount)
        {
            if (furnitureAmount - itemAmount<0)
            {
                return false;
            }
            return true;
        }

        public void DecreaseQuantity(Furniture furniture)
        {
            ShoppingCarItem item = items.Where(p => p.Furniture.FurnitureId == furniture.FurnitureId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity -= 1;
            }
            if (item.Quantity == 0)
            {
                RemoveItem(furniture);
            }
        }

        public decimal ComputeTotalValue()
        {
            return items.Sum(e => e.Furniture.Price * e.Quantity);
        }

        public decimal ComputeTotalValueWithTax()
        {
            decimal total = ComputeTotalValue();
            return total + total * Tax.RegularTax;
        }

        public void Clear()
        {
            items.Clear();
        }
        public decimal SumQuantity()
        {
            return items.Sum(x => x.Quantity);

        }
        public IEnumerable<ShoppingCarItem> ViewItems
        {
            get { return Viewitems; }
        }
        public IEnumerable<ShoppingCarItem> Items
        {
            get { return items; }
        }


    }

    public class ShoppingCarItem
    {
        public Furniture Furniture { get; set; }
        public int Quantity { get; set; }
    }
   
}