using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Models.Order
{
    public class OrderCreateBindingModel
    {
        [Required]
        public string FlowerId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Order")]
        public int OrdersCount { get; set; }
    }
}
