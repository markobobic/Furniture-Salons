using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInItProjekat.Models
{
    public class BillItem
    {
        [Key]
        public int BillItemId { get; set; }
        [Required]
        [Display(Name ="Furniture: ")]
        public string Furniture { get; set; }
        [Required]
        [Display(Name = "Price: ")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Amount: ")]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "Furniture shop: ")]
        public string FurnitureShop { get; set; }
        [ForeignKey("Bill")]
        public int BillId { get; set; }
        [Required]
        [Display(Name = "Furniture category: ")]
        public string FurnitureCategory { get; set; }

        public virtual Bill Bill { get; set; }
    }
}