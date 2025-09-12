using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            var teams = _context.Teams
                .Include(e => e.TeamId)
                .Include(e => e.Name)
                .Include(e => e.ManagerId)
                .ToList();
            return Ok(Teams);
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _context.Teams
                .Include(e => e.TeamId)
                .Include(e => e.Name)
                .Include(e => e.ManagerId)
                .FirstOrDefault(e => e.TeamId == id);

            if (Team == null) return NotFound();
            return Ok(Team);
        }

        [HttpPost]
        public ActionResult<Team> CreateTeam(Team Team)
        {
            _context.Teams.Add(Team);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTeam), new { id = Team.Team_Id }, Team);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeamManager(int TeamId, int ManagerId)
        {
            var Team = _context.Teams.Find(TeamId);
            if (Team == null) return BadRequest();
            Team.ManagerId = id;
            _context.Entry(Team).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var Team = _context.Teams.Find(id);
            if (Team == null) return NotFound();

            _context.Teams.Remove(Team);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
