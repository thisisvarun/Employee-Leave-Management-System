using backend.Models;
using backend.Service;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveService _leaveService;

        public EmployeeController(IEmployeeService employeeService, ILeaveService leaveService)
        {
            _employeeService = employeeService;
            _leaveService = leaveService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("{id}/summary")]
        public async Task<ActionResult<LeaveSummaryDto>> GetLeaveSummary(int id)
        {
            var summary = await _leaveService.GetLeaveSummaryAsync(id);
            return Ok(summary);
        }
    }
}
