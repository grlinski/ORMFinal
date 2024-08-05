using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORMFinal.Models
{
    public class AnimalHealth
    {
        [Key]
        public int HealthReportId { get; set; }

        //[Required]
        [StringLength(50)]
        public string HealthStatus { get; set; }

        public DateTime ReportDate { get; set; }
        public DateTime LastVaccinationDate { get; set; }

        [ForeignKey("AnimalId")]
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
