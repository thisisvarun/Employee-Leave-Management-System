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
        public int? Team_Id { get; set; } // null if employee is not a part of any team (on bench)
        public int Salary { get; set; }
        public int Designation_Id { get; set; }
        public DateTime Date_Of_Joining { get; set; }
        public bool Is_Active { get; set; }
        public string Password_Hash { get; set; } = string.Empty;
    }
}