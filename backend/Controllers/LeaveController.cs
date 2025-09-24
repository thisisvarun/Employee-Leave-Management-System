using backend.DTOs;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveDto leaveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leaveId = await _leaveService.ApplyLeaveAsync(leaveDto);

            if (leaveId == -1)
            {
                return BadRequest(new { message = "Insufficient leave balance when considering pending requests." });
            }

            return Ok(new { LeaveId = leaveId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeave(int id, [FromBody] LeaveDto leaveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _leaveService.UpdateLeaveAsync(id, leaveDto);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelLeave(int id)
        {
            var result = await _leaveService.CancelLeaveAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}/history")]
        public async Task<ActionResult<List<LeaveHistoryDto>>> GetLeaveHistoryAsync(int id)
        {
            var history = await _leaveService.GetLeaveHistoryAsync(id);
            if (history is null || history.Count == 0)
            {
                return NotFound();
            }
            Console.WriteLine(history.Count);
            return Ok(history);
        }
    }
}