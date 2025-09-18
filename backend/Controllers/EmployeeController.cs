<<<<<<< Updated upstream
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using backend.Models;
// using System.Collections.Generic;
// using System.Linq;

// namespace backend.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EmployeesController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public EmployeesController(AppDbContext context)
//         {
//             _context = context;
//         }

//         [HttpGet]
//         public ActionResult<IEnumerable<Employee>> GetEmployees()
//         {
//             var employees = _context.Employees
//                 .Include(e => e.Team_Id)
//                 .Include(e => e.Designation_Id)
//                 .ToList();
//             return Ok(employees);
//         }

//         [HttpGet("{id}")]
//         public ActionResult<Employee> GetEmployee(int id)
//         {
//             var employee = _context.Employees
//                 .Include(e => e.Team_Id)
//                 .Include(e => e.Designation_Id)
//                 .FirstOrDefault(e => e.Employee_Id == id);

//             if (employee == null) return NotFound();
//             return Ok(employee);
//         }

//         [HttpPost]
//         public ActionResult<Employee> CreateEmployee(Employee employee)
//         {
//             _context.Employees.Add(employee);
//             _context.SaveChanges();
//             return CreatedAtAction(nameof(GetEmployee), new { id = employee.Employee_Id }, employee);
//         }

//         [HttpPut("{id}")]
//         public IActionResult UpdateEmployeeTeam(int Team_Id, int Employee_Id)
//         {
//             var employee = _context.Employees.Find(Employee_Id);
//             if (employee == null) return BadRequest();
//             employee.Team_Id = Team_Id;
//             _context.Entry(employee).State = EntityState.Modified;
//             _context.SaveChanges();
//             return NoContent();
//         }

//         [HttpPut("{id}")]
//         public IActionResult UpdateEmployeeDesignation(int Designation_Id, int Employee_Id)
//         {
//             var employee = _context.Employees.Find(Employee_Id);
//             if (employee == null) return BadRequest();
//             employee.Designation_Id = Designation_Id;
//             _context.Entry(employee).State = EntityState.Modified;
//             _context.SaveChanges();
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public IActionResult DeleteEmployee(int id)
//         {
//             var employee = _context.Employees.Find(id);
//             if (employee == null) return NotFound();

//             _context.Employees.Remove(employee);
//             _context.SaveChanges();

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
            return Ok("Employee deleted");
        }
    }
}
>>>>>>> Stashed changes
