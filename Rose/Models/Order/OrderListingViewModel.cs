using Rose.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Models.Order
{
    public class OrderListingViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int FlowerId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        public string FlowerName { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [MinLength(10)]
        [MaxLength(50)]
        public string OrderDate { get; set; }

    }
}
