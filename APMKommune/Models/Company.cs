using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Kommune")]
        public String Name { get; set; }

        [Display(Name = "Kommunenummer")]
        public String CompanyNr { get; set; }

        // Navigation Property
        public List<Application> applications { get; set; }
    }
}