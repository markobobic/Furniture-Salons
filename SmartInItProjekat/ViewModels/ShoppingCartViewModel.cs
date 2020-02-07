using SmartInItProjekat.Infrastructure;
using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInItProjekat.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public string ReturnUrl { get; set; }
        public Furniture Furniture { get; set; }
    }
}