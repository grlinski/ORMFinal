using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORMFinal.Models
{
    public class FeedingSchedule
    {
        [Key]
        public int FeedingScheduleId { get; set; }
        public DateTime? MorningFeeding { get; set; }
        public DateTime? NoonFeeding { get; set; }
        public DateTime? EveningFeeding { get; set; }
        public DateTime? NightFeeding { get; set; }


        [Required]
        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
