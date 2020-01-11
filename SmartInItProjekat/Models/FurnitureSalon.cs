using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    public class FurnitureSalon
    {
        [Key]
        public int FurnitureSalonId { get; set; }
        [Display(Name="Name: ")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Owner: ")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        
        public string Owner { get; set; }
        [Required]
        [Display(Name = "Adress: ")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Adress { get; set; }
        [Required]
        [Display(Name = "Telephone number: ")]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        public string TelephoneNumber { get; set; }
        [Required]
        [Display(Name="Email: ")]
        [StringLength(maximumLength:50,MinimumLength =6)]
        public string Email { get; set; }
        [Display(Name="Web page: ")]
        public string WebPage { get; set; }
        [Unique(ErrorMessage = "This already exist !!")]
        [Display(Name ="PIB :")]
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 7)]
        public string PIB { get; set; }
        [Unique(ErrorMessage = "This already exist !!")]
        [Display(Name = "Account number :")]
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 7)]
        public string AccountNumber  { get; set; }
        public virtual ICollection<Furniture> Furnitures { get; set; }
       
        public FurnitureSalon()
        {
            this.Furnitures = new HashSet<Furniture>();
        }

    }
}