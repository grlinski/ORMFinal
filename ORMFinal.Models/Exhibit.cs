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
        public string Size { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

        //Nav
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
