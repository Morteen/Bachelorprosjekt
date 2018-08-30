using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Segment
    {
        [Key]
        public int SegmentId { get; set; }

        [Required]
        [Display(Name = "Etat")]
        public String Name { get; set; }

        // Navigation Property
        public List<Application> applications { get; set; }
        public List<Service> services { get; set; }
    }
}