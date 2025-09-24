using backend.DTOs;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("leaves")]
        public async Task<IActionResult> GetTeamLeaveRequests()
        {
            var managerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(managerId))
            {
                return Unauthorized();
            }

            var leaveRequests = await _teamService.GetTeamLeaveRequestsAsync(int.Parse(managerId));
            return Ok(leaveRequests);
        }

        [HttpPut("leaves/{leaveId}/status")]
        public async Task<IActionResult> UpdateLeaveStatus(int leaveId, [FromBody] UpdateLeaveStatusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Console.WriteLine("Claims   " + User.FindFirstValue(ClaimTypes.Role) + " " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _teamService.UpdateLeaveStatusAsync(leaveId, dto);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

