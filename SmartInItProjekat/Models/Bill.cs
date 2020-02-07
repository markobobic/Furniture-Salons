using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartInItProjekat.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        [Required]
        [Display(Name ="Tax: ")]
        public decimal Tax { get; set; }
        [Required]
        [Display(Name ="Total price: ")]
        public decimal TotalPrice { get; set; }
        [Display(Name ="Time of purchase: ")]
        public DateTime TimeOfPurchase { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        [Display(Name ="Buyer: ")]
        public string Buyer { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
        public decimal TotalTax()
        {
            return Subtotal() * 0.2M;
        }
        public Bill()
        {
            this.BillItems = new HashSet<BillItem>();
        }
        public decimal Subtotal()
        {
            return BillItems.Sum(m => m.Price);
        }
    }
}