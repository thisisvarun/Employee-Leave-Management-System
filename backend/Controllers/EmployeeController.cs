<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.Data.SqlClient;
=======
<<<<<<< Updated upstream
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using backend.Models;
// using System.Collections.Generic;
// using System.Linq;
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly string _connectionString = "";

        public EmployeeController()
        {
            try
            {
                _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new Exception("Failed to connect to database!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect to the SQL Database: {e.Message}");
            }
        }


        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        FirstName = reader["FirstName"].ToString() ?? "",
                        LastName = reader["LastName"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                        TeamId = reader["TeamId"] as int?,
                        Salary = (int)reader["Salary"],
                        DesignationId = (int)reader["DesignationId"],
                        DateOfJoining = (DateTime)reader["DateOfJoining"],
                        Active = (bool)reader["Active"],
                        PasswordHash = reader["PasswordHash"].ToString() ?? ""
                    });
                }
            }

            return employees;
        }

        [HttpGet("{id}")]
        public Employee? GetById(int id)
        {
            Employee? employee = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE EmployeeId=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        FirstName = reader["FirstName"].ToString() ?? "",
                        LastName = reader["LastName"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["Phone"].ToString(),
                        TeamId = reader["TeamId"] as int?,
                        Salary = (int)reader["Salary"],
                        DesignationId = (int)reader["DesignationId"],
                        DateOfJoining = (DateTime)reader["DateOfJoining"],
                        Active = (bool)reader["Active"],
                        PasswordHash = reader["PasswordHash"].ToString() ?? ""
                    };
                }
            }

<<<<<<< HEAD
            return employee;
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Employee (FirstName, LastName, Email, Phone, TeamId, Salary, DesignationId, DateOfJoining, Active, PasswordHash) 
                      VALUES (@FirstName, @LastName, @Email, @Phone, @TeamId, @Salary, @DesignationId, @DateOfJoining, @Active, @PasswordHash)", conn);

                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Phone", (object?)employee.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TeamId", (object?)employee.TeamId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DesignationId", employee.DesignationId);
                cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                cmd.Parameters.AddWithValue("@Active", employee.Active);
                cmd.Parameters.AddWithValue("@PasswordHash", employee.PasswordHash);

                cmd.ExecuteNonQuery();
            }

            return Ok("Employee created");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Employee SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, 
                      TeamId=@TeamId, Salary=@Salary, DesignationId=@DesignationId, DateOfJoining=@DateOfJoining, 
                      Active=@Active, PasswordHash=@PasswordHash WHERE EmployeeId=@Id", conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Phone", (object?)employee.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TeamId", (object?)employee.TeamId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DesignationId", employee.DesignationId);
                cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                cmd.Parameters.AddWithValue("@Active", employee.Active);
                cmd.Parameters.AddWithValue("@PasswordHash", employee.PasswordHash);

                cmd.ExecuteNonQuery();
            }

            return Ok("Employee updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeId=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

=======
//             return NoContent();
//         }
//     }
// }
=======
﻿using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _service.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{emp_Id}")]
        public IActionResult GetEmployeeById(int emp_Id)
        {
            var employee = _service.GetEmployeeById(emp_Id);
            if (employee == null) return NotFound("Employee not found");
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeCreateDTO dto)
        {
            _service.CreateEmployee(dto);
            return Ok("Employee created");
        }

        [HttpPut("update")]
        public IActionResult UpdateEmployee(EmployeeUpdateDTO dto)
        {
            bool updated = _service.UpdateEmployee(dto);
            if (!updated) return NotFound("Employee not found");
            return Ok("Employee updated");
        }

        [HttpPut("password")]
        public IActionResult UpdatePassword(EmployeeUpdatePasswordDTO dto)
        {
            bool success = _service.UpdatePassword(dto);
            if (!success) return BadRequest("Old password is incorrect");
            return Ok("Password updated");
        }

        [HttpDelete("{emp_Id}")]
        public IActionResult DeleteEmployee(int emp_Id)
        {
            bool deleted = _service.DeleteEmployee(emp_Id);
            if (!deleted) return NotFound("Employee not found");
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
            return Ok("Employee deleted");
        }
    }
}
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
