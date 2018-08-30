using APMKommune.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APMKommune.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        [Display(Name = "Applikasjon")]
        public String Name { get; set; }

        [Display(Name = "Beskrivelse")]
        public String Description { get; set; }

        [Display(Name = "Antall brukere")]
        public int NumberOfUsers { get; set; }

        [Display(Name = "Driftes av")]
        public String OperatedBy { get; set; }

        [Display(Name = "Avtaleinformasjon")]
        public String ContractInformation { get; set; }

        [Display(Name = "Informasjonslink")]
        public String InfoLink { get; set; }

        [Display(Name = "Status")]
        public String Status { get; set; }

        [Display(Name = "Systemansvarlig")]
        public String Administrator { get; set; }

        [Display(Name = "Superbrukere")]
        public String SuperUsers { get; set; } //TODO: Bør denne være en liste?

        [Display(Name = "Type applikasjon")]
        public String Type { get; set; }

        [Display(Name = "Sist oppgradert")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime LastUpgraded { get; set; }

        [Display(Name = "Eksterne brukere")]
        public String ExternalUsers { get; set; } //TODO: Bør denne være en liste? Eller boolean?

        [Display(Name = "Årlig kostnad")]
        public float CostYearly { get; set; }

        [Display(Name = "Initiell kostnad")]
        public float CostInitial { get; set; }

        [Display(Name = "Bruker felleskomponenter")]
        public Boolean UsesSharedComponents { get; set; }

        [Display(Name = "Styrker")]
        public String Strengths { get; set; }

        [Display(Name = "Svakheter")]
        public String Weaknesses { get; set; }

        [Display(Name = "Oppsigelse")]
        public String ContractResignation { get; set; }

        [Display(Name = "Poeng for Forretningsverdi")]
        public int BusinessValueScore { get; set; }

        [Display(Name = "Poeng for passende arkitektur")]
        public int ArchitectureFitsScore { get; set; }

        [Display(Name = "Poeng for applikasjonsrisiko")]
        public int ApplicationRiskScore { get; set; }

        [Display(Name = "Poeng for applikasjonshastighet")]
        public int ApplicationSpeedScore { get; set; }


        // Foreign Keys

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("Segment")]
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }



        // Navigation properties
        public List<Integration> Integration { get; set; }
        public List<Dataset> Datasets { get; set; }
    }
}