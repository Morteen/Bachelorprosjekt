using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [Display(Name = "Tjeneste")]
        public String Name { get; set; }

        // Foreign key
        [ForeignKey("Segment")]
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }

        // Navigation Property
        public List<Application> applications { get; set; }
    }
}