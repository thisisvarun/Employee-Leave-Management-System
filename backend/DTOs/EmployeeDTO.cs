using System;

namespace backend.DTOs
{
    public class EmployeeCreateDTO
    {
<<<<<<< HEAD
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
=======
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
        public int Team_Id { get; set; }
        public int Salary { get; set; }
        public int Designation_Id { get; set; }
        public DateTime Date_Of_Joining { get; set; }
        public bool Is_Active { get; set; }
<<<<<<< HEAD
        public string Password { get; set; } = string.Empty; //hash it before saving in DB
=======
        public string Password { get; set; } //hash it before saving in DB
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
    }

    public class EmployeeReadDTO
    {
        public int Employee_Id { get; set; }
<<<<<<< HEAD
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty; //get name from Designation Table
        public string Team { get; set; } = string.Empty; //get name from Team Table
=======
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; } //get name from Designation Table
        public string Team { get; set; } //get name from Team Table
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
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
<<<<<<< HEAD
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
=======
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
    }
}