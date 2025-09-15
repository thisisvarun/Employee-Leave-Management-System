using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeavesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Leave>> GetLeaves()
        {
            var Leaves = _context.Leaves.ToList();
            return Ok(Leaves);
        }

        [HttpGet("{id}")]
        public ActionResult<Leave> GetLeave(int id)
        {
            var Leave = _context.Leaves
                .FirstOrDefault(e => e.LeaveRequestId == id);

            if (Leave == null) return NotFound();
            return Ok(Leave);
        }

        [HttpPost]
        public ActionResult<Leave> CreateLeave(Leave leave)
        {
            _context.Leaves.Add(leave);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLeave), new { id = Leave.LeaveRequestId }, leave);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeave(int LeaveRequestId, int Employee_Id)
        {
            var Leave = _context.Leaves.Find(Leave_Id);
            if (Leave == null) return BadRequest();
            Leave.Team_Id = Team_Id;
            _context.Entry(Leave).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveDesignation(int Designation_Id, int Leave_Id)
        {
            var Leave = _context.Leaves.Find(Leave_Id);
            if (Leave == null) return BadRequest();
            Leave.Designation_Id = Designation_Id;
            _context.Entry(Leave).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeave(int id)
        {
            var Leave = _context.Leaves.Find(id);
            if (Leave == null) return NotFound();

            _context.Leaves.Remove(Leave);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
