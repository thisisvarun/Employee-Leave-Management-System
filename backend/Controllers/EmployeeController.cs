using backend.Models;
using backend.Service;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(EmployeeService service) : ControllerBase
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
            var employees = await EmployeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _service.GetEmployeeById(id);
            if (employee == null) return NotFound(new { message = "Employee not found" });

            return Ok(employee);
        }

        [HttpGet("{id}/summary")]
        public async Task<ActionResult<LeaveSummaryDto>> GetLeaveSummary(int id)
        {
            var summary = await _leaveService.GetLeaveSummaryAsync(id);
            return Ok(summary);
        }

        [HttpGet("{id}/recent-leave-status")]
        public async Task<ActionResult<Leave>> GetRecentLeaveStatus(int id)
        {
            var leave = await _leaveService.GetMostRecentProcessedLeaveAsync(id);
            if (leave is null)
            {
                Console.WriteLine("well well well!");
                return NotFound();
            }
            Console.WriteLine(leave.Comment);
            return Ok(leave);
        }
    }
}
