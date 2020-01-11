using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniqueAttribute.Attribute;

namespace SmartInItProjekat.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Name: ")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Name: ")]
        [StringLength(maximumLength: 1000)]
        public string Description { get; set; }

        public virtual ICollection<Furniture> Furnitures { get; set; }

        public Category()
        {
            this.Furnitures = new HashSet<Furniture>();
        }
    }
}