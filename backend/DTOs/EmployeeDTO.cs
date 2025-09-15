using System;

namespace backend.DTOs
{
    public class EmployeeCreateDTO
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Team_Id { get; set; }
        public int Salary { get; set; }
        public int Designation_Id { get; set; }
        public DateTime Date_Of_Joining { get; set; }
        public bool Is_Active { get; set; }
        public string Password { get; set; } //hash it before saving in DB
    }

    public class EmployeeReadDTO
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; } //get name from Designation Table
        public string Team { get; set; } //get name from Team Table
        public int Salary { get; set; }
        public DateTime Date_Of_Joining { get; set; }
        public bool Is_Active { get; set; }
    }

    public class EmployeeUpdateDTO
    {
        public int Employee_Id { get; set; }
        public string? Phone { get; set; }
        public int? Team_Id { get; set; }
        public int? Salary { get; set; }
        public int? Designation_Id { get; set; }
        public bool? Is_Active { get; set; }
    }

    public class EmployeeDeleteDTO
    {
        public int Employee_Id { get; set; }
    }

    public class EmployeeUpdatePasswordDTO
    {
        public int Employee_Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}