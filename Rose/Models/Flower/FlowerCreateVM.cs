using Microsoft.AspNetCore.Http;
using Rose.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Models.Flower
{
    public class FlowerCreateVM
    {
        public FlowerCreateVM()
        {
            Categories = new List<CategoryPairVM>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public virtual List<CategoryPairVM> Categories { get; set; }
    }
}
