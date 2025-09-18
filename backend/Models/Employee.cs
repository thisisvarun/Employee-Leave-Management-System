using System;

namespace backend.Models
{
    public class Employee
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? Team_Id { get; set; }
        public int Salary { get; set; }
        public int Designation_Id { get; set; }
        public DateTime Date_Of_Joining { get; set; }

        [Required]
        public bool Active { get; set; }
<<<<<<< Updated upstream

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [ForeignKey("Team_Id")]
        public Team? Team { get; set; }

        [ForeignKey("Designation_Id")]
        public Designation? Designation { get; set; }

        public ICollection<Leave>? Leaves { get; set; } = new List<Leave>();
=======
        public string Password_Hash { get; set; } = string.Empty;
>>>>>>> Stashed changes
    }
}
