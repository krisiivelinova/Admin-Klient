using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Entities
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey("Flower")]
        public int FlowerId { get; set; }
        public virtual Flower Flower { get; set; }
        public string Extension { get; set; }

    }
}
