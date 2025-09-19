using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeavesController(LeaveService leaveService) : ControllerBase
    {
        private readonly LeaveService _leaveService = leaveService;

        // Get all leave requests
        [HttpGet]
        public ActionResult<IEnumerable<LeaveReadDTO>> GetLeaves()
        {
            var leaves = _leaveService.GetAll();
            return Ok(leaves);
        }

        // Get leave request by ID
        [HttpGet("{id}")]
        public ActionResult<LeaveReadDTO> GetLeave(int id)
        {
            var leave = _leaveService.GetById(id);
            if (leave == null) return NotFound("Leave request not found");
            return Ok(leave);
        }

        // Create leave request with multiple dates
        [HttpPost]
        public ActionResult<int> CreateLeave([FromBody] LeaveCreateWithDatesDTO dto)
        {
            if (dto == null) return BadRequest("Invalid leave request data");

            int newLeaveId = _leaveService.Create(dto);
            return Ok(new { LeaveRequest_Id = newLeaveId, message = "Leave request created" });
        }

        // Update leave request
        [HttpPut("{id}")]
        public IActionResult UpdateLeave(int id, [FromBody] LeaveUpdateDTO dto)
        {
            dto.LeaveRequest_Id = id;
            var updated = _leaveService.Update(dto);
            if (!updated) return NotFound("Leave request not found");
            return Ok("Leave request updated");
        }

        // Delete leave request
        [HttpDelete("{id}")]
        public IActionResult DeleteLeave(int id)
        {
            var deleted = _leaveService.Delete(id);
            if (!deleted) return NotFound("Leave request not found");
            return Ok("Leave request deleted");
        }
    }
}
