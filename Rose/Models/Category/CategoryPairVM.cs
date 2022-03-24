using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Models.Category
{
    public class CategoryPairVM
    {
        public int Id { get; set; }

        [Display(Name="Breed")]
        public string Name { get; set; }
    }
}
