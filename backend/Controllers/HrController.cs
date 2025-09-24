using backend.DTOs;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Hr")]
    public class HrController : ControllerBase
    {
        private readonly IHrService _HrService;

        public HrController(IHrService HrService)
        {
            _HrService = HrService;
        }

        [HttpGet("leaves")]
        public async Task<IActionResult> GetHrLeaveRequests()
        {
            var hrId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(hrId))
            {
                return Unauthorized();
            }

            var leaveRequests = await _HrService.GetHrLeaveRequestsAsync(int.Parse(hrId));
            return Ok(leaveRequests);
        }

        [HttpPut("leaves/{leaveId}/status")]
        public async Task<IActionResult> UpdateLeaveStatus(int leaveId, [FromBody] UpdateLeaveStatusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Console.WriteLine("Claims   " + User.FindFirstValue(ClaimTypes.Role) + " " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _HrService.UpdateLeaveStatusAsync(leaveId, dto);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
        // api/hr/leaves
    }
}

