using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "You must provide category name")]
        [Display(Name = "Name ")]
        [Remote("DoesNameExist", "Categories", ErrorMessage = "Name already exists!")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters!")]
        public string Name { get; set; }
        [Display(Name = "Description ")]
        [StringLength(maximumLength: 1000)]
        public string Description { get; set; }

        public virtual ICollection<Furniture> Furnitures { get; set; }

        public Category()
        {
            this.Furnitures = new HashSet<Furniture>();
        }
    }
}