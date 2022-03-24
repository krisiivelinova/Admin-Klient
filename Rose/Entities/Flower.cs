﻿using System;
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
        //[Display(Name = "Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        [Range(1, 50)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(30)]
        public int Quantity { get; set; }
        
    }
}
