using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Entities
{
    public class Flower
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(50)]
        public int Quantity { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }



    }
}
