using Microsoft.AspNetCore.Mvc;
using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeCreateDTO dto)
        {
            var createdEmployee = _employeeService.CreateEmployee(dto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Employee_Id }, createdEmployee);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeUpdateDTO dto)
        {
            bool success = _employeeService.UpdateEmployee(dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            bool success = _employeeService.DeleteEmployee(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("updatePassword")]
        public IActionResult UpdatePassword([FromBody] EmployeeUpdatePasswordDTO dto)
        {
            bool success = _employeeService.UpdatePassword(dto);
            if (!success) return BadRequest(new { message = "Invalid old password or employee not found" });
            return NoContent();
        }
    }
}