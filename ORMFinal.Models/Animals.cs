using System.ComponentModel.DataAnnotations;

namespace ORMFinal.Models
{
    public class Animal
    {
        [Key]
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


        //Nav properties between tables/models
        public virtual ICollection<Exhibit> Exhibits { get; set; }
        public virtual FeedingSchedule FeedingSchedule { get; set; }
        public virtual AnimalHealth AnimalHealth { get; set; }
    }
}
