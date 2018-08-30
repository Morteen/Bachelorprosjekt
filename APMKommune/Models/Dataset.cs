using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Dataset
    {
        [Key]
        public int DatasetId { get; set; }

        [Required]
        [Display(Name = "Datasett")]
        public String Name { get; set; }

        [Display(Name = "Beskrivelse")]
        public String Description { get; set; }

        [Display(Name = "Masterdata")]
        public Boolean isMasterData { get; set; }

        [Display(Name = "Tilgjengelig")]
        public Boolean isAccessible { get; set; }


        // Foreign keys

        [ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

    }
}