using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    public class FurnitureSalon
    {
        [Key]
        public int FurnitureSalonId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Required(ErrorMessage = "You must provide name")]
        [Display(Name = "Name ")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must provide owner name")]
        [Display(Name = "Owner ")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters!")]
        public string Owner { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\w+( +\w+)*$|", ErrorMessage = "Spaces can only exist beetween words")]
        [Required(ErrorMessage = "You must provide address")]
        [Display(Name = "Address ")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters!")]

        public string Adress { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Telephone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string TelephoneNumber { get; set; }
        [Display(Name = "Email address")]
        [Remote("DoesEmailExist", "FurnitureSalons", ErrorMessage = "E-mail address already exists!")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Display(Name="Web page ")]
        [RegularExpression(@"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$",ErrorMessage = "Provide valid website url!")]
        public string WebPage { get; set; }
        [Remote("DoesPIBExist", "FurnitureSalons", ErrorMessage = "PIB already exists!")]
        [RegularExpression(@"^[0-9]+$",ErrorMessage = "PIB must be a number")]
        [Required(ErrorMessage = "You must provide PIB")]
        [Display(Name = "PIB ")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "PIB number must be 8 characters long!")]
        public string PIB { get; set; }
        [Remote("DoesAccountNumberExist", "FurnitureSalons", ErrorMessage = "Account number already exists!")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Account number must be a number")]
        [Required(ErrorMessage = "You must provide account number")]
        [Display(Name = "Account number")]
        [StringLength(maximumLength: 15, MinimumLength = 8, ErrorMessage = "Account number must be between 7 and 15 characters!")]
        public string AccountNumber  { get; set; }
        public virtual ICollection<Furniture> Furnitures { get; set; }
       
        public FurnitureSalon()
        {
            this.Furnitures = new HashSet<Furniture>();
        }

    }
}