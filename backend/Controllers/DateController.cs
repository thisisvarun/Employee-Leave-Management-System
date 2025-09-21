using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatesController : ControllerBase
    {
        private readonly DateService _service;

        public DatesController(DateService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<DateReadDTO> AddDate(DateCreateDTO dto)
        {
            var createdDate = _service.AddDate(dto);
            return Ok(createdDate);
        }

        [HttpGet("leave/{leaveId}")]
        public ActionResult<List<DateReadDTO>> GetDatesByLeaveId(int leaveId)
        {
            var dates = _service.GetDatesByLeaveId(leaveId);
            return Ok(dates);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDate(int id)
        {
            bool deleted = _service.DeleteDate(id);
            if (!deleted) return NotFound("Date not found");
            return Ok("Date deleted");
        }
    }
}
