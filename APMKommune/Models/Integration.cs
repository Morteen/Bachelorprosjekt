using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Integration
    {
        [Key]
        public int IntegrationId { get; set; }

        [Required]
        [Display(Name = "Integrasjon")]
        public String Name { get; set; }
        
        [Display(Name = "Type")]
        public String Type { get; set; }
        
        [Display(Name = "Beskrivelse")]
        public String Description { get; set; }
        [Display(Name="Target Applikasjon")]
        public int TargetApplicationId { get; set; }
        public int ApplicationId { get; set; }






        // Foreign keys



        [ForeignKey("ApplicationId")]
        public  Application Application { get; set; }
        [ForeignKey("TargetApplicationId")]
        public Application TargetApplication { get; set; }


        //TODO: Ordne fremmednøkler i Integration
    }
}