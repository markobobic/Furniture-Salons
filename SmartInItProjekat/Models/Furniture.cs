using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    
    public class Furniture
    {
        [Key]
        public int FurnitureId { get; set; }
        [Unique(ErrorMessage = "This already exist !!")]
        [Required]
        [Display(Name ="Code: ")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Name: ")]
        [StringLength(maximumLength:50,MinimumLength =2)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Country of origin: ")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string CountryOfOrigin { get; set; }
        [Required]
        [Display(Name = "Year of Manufacture: ")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Amount: ")]
        public int Amount { get; set; }
        [Required]
        [Display(Name="Price")]
        public decimal Price { get; set; }

        public string PhotoType { get; set; }
        public byte[] ProductImage { get; set; }

        [ForeignKey("FurnitureSalon")]
        public int FurnitureSalonId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual FurnitureSalon FurnitureSalon { get; set; }
    }
}