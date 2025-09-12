using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Column("Employee_Id")]
        public int Employee_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string First_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Last_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        public int? Team_Id { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public int Designation_Id { get; set; }

        [Required]
        public DateTime Date_Of_Joining { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [ForeignKey("Team_Id")]
        public Team? Team { get; set; }

        [ForeignKey("Designation_Id")]
        public Designation? Designation { get; set; }

        public ICollection<Leave>? Leaves { get; set; } = new List<Leave>();
    }
}
