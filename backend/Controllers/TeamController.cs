using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Failed to connect to database!");
            var repository = new TeamRepository(connectionString);
            _teamService = new TeamService(repository);
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var teams = _teamService.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
                return NotFound(new { message = "Team not found" });

            return Ok(team);
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] TeamCreateDTO dto)
        {
            bool created = _teamService.CreateTeam(dto);
            if (!created)
                return BadRequest(new { message = "Failed to Create Team!" });
            return Ok(new { message = "Team created" });
        }

        [HttpPut]
        public IActionResult UpdateTeam([FromBody] TeamUpdateDTO dto)
        {
            bool updated = _teamService.UpdateTeam(dto);
            if (!updated)
                return NotFound(new { message = "Team not found" });

            return Ok(new { message = "Team updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            bool deleted = _teamService.DeleteTeam(id);
            if (!deleted)
                return NotFound(new { message = "Team not found" });

            return Ok(new { message = "Team deleted" });
        }
    }
}
