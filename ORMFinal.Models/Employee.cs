using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORMFinal.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        [ForeignKey("Exhibit")]
        public int? ExhibitId { get; set; }
        public virtual Exhibit? Exhibit { get; set; }
    }
}
