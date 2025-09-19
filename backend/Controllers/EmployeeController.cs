using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(EmployeeService service) : ControllerBase
    {
        private readonly EmployeeService _service = service;

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _service.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _service.GetEmployeeById(id);
            if (employee == null) return NotFound(new { message = "Employee not found" });

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeCreateDTO dto)
        {
            int newEmployeeId = _service.CreateEmployee(dto); // service returns new employee ID
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployeeId }, dto);
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeUpdateDTO dto)
        {
            bool updated = _service.UpdateEmployee(dto);
            if (!updated) return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee updated successfully" });
        }

        [HttpPut("password")]
        public IActionResult UpdatePassword([FromBody] EmployeeUpdatePasswordDTO dto)
        {
            bool success = _service.UpdatePassword(dto);
            if (!success) return BadRequest(new { message = "Old password is incorrect" });

            return Ok(new { message = "Password updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            bool deleted = _service.DeleteEmployee(id);
            if (!deleted) return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}