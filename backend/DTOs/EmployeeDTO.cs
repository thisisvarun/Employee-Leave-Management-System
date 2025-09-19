using System;

namespace backend.DTOs
{
    public class EmployeeCreateDTO
    {
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? Team_Id { get; set; }
        public int Salary { get; set; }
        public int Designation_Id { get; set; }
        public DateTime Date_Of_Joining { get; set; }
        public bool Is_Active { get; set; }
        public string Password { get; set; } = string.Empty; //hash it before saving in DB
    }

    public class EmployeeReadDTO
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty; //get name from Designation Table
        public string Team { get; set; } = string.Empty; //get name from Team Table , Bench if ID is null
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
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}