using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [Display(Name = "Leverandør")]
        public String Name { get; set; }

        // Navigation Property
        public List<Application> applications { get; set; }
    }
}