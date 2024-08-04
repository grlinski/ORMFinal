using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORMFinal.Models
{
    public class Exhibit
    {
        [Key]
        public int ExhibitId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        public string Size { get; set; }

        [Required(ErrorMessage = "The Animal field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an animal.")]
        [Display(Name = "Animal")]
        public int AnimalId { get; set; }

        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
    }

}
