using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    
    public class Furniture
    {
        [Key]
        public int FurnitureId { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage ="Only numbers and no spaces!")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "You must provide furniture code")]
        [Remote("DoesCodeExist", "Furnitures", ErrorMessage = "Code already exists!")]
        [StringLength(maximumLength: 50, MinimumLength = 6,ErrorMessage ="Code must be between 6 and 50 characters!")]
        [Display(Name ="Code ")]
        public string Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Display(Name = "Name ")]
        [StringLength(maximumLength:50,MinimumLength =2, ErrorMessage = "Name must be between 6 and 50 characters!")]
        [Required(ErrorMessage = "You must provide furniture name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must provide country")]
        [Display(Name = "Country of origin ")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$", ErrorMessage = "Spaces can only exist beetween words")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 50 characters!")]
        public string CountryOfOrigin { get; set; }
        [Required(ErrorMessage = "You must provide year of manufacture")]
        [Range(1900, 2020, ErrorMessage = "Year must be between 1900 and 2020!")]
        [Display(Name = "Year of Manufacture ")]
        public int Year { get; set; }
        [Required(ErrorMessage = "You must provide furniture amount")]
        [Range(0, 10000, ErrorMessage = "Amount should be between 0 and 10 000!")]
        [Display(Name = "Amount ")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "You must provide furniture price")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price should between 1 and 999999999")]
        [Display(Name="Price ")]
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