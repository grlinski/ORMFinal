using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFinal.Models {
    public class AnimalViewModel {
        public int AnimalId { get; set; }

        [Required]
        [StringLength(100)]
        public string AnimalCategory { get; set; }

        [Required]
        [StringLength(100)]
        public string Habitat { get; set; }

        [Required]
        [StringLength(100)]
        public string AnimalName { get; set; }

        [Required]
        [StringLength(100)]
        public string Species { get; set; }

        [Required]
        [StringLength(100)]
        public string Genus { get; set; }

        // AnimalHealth properties
        public string HealthStatus { get; set; } = "Healthy";
        public DateTime? ReportDate { get; set; } = DateTime.Now;
        public DateTime? LastVaccinationDate { get; set; } = DateTime.Now;

        // FeedingSchedule properties
        public DateTime? MorningFeeding { get; set; } = DateTime.Now;
        public DateTime? NoonFeeding { get; set; } = DateTime.Now;
        public DateTime? EveningFeeding { get; set; } = DateTime.Now;
        public DateTime? NightFeeding { get; set; } = DateTime.Now;
    }
}
