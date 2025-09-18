using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly string _connectionString;

        public TeamController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception("Failed to connect to database!");
        }

        [HttpGet]
        public List<TeamReadDTO> GetAllTeams()
        {
            var teams = new List<TeamReadDTO>();

            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("SELECT * FROM Team", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    teams.Add(new TeamReadDTO
                    {
                        Team_Id = (int)reader["Team_Id"],
                        Team_Name = reader["Team_Name"].ToString() ?? "",
                        Manager_Id = reader["Manager_Id"] as int?
                    });
                }
            }

            return teams;
        }

        [HttpGet("{id}")]
        public TeamReadDTO? GetTeamById(int id)
        {
            TeamReadDTO? team = null;

            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("SELECT * FROM Team WHERE Team_Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    team = new TeamReadDTO
                    {
                        Team_Id = (int)reader["Team_Id"],
                        Team_Name = reader["Team_Name"].ToString() ?? "",
                        Manager_Id = reader["Manager_Id"] as int?
                    };
                }
            }

            return team;
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamCreateDTO dto)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("INSERT INTO Team (Team_Name, Manager_Id) VALUES (@Team_Name, @Manager_Id)", conn);
                cmd.Parameters.AddWithValue("@Team_Name", dto.Team_Name);
                cmd.Parameters.AddWithValue("@Manager_Id", (object?)dto.Manager_Id ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            return Ok("Team created");
        }

        [HttpPut]
        public IActionResult UpdateTeam(TeamUpdateDTO dto)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new(@"UPDATE Team 
                                       SET Team_Name=@Team_Name, Manager_Id=@Manager_Id 
                                       WHERE Team_Id=@Team_Id", conn);

                cmd.Parameters.AddWithValue("@Team_Id", dto.Team_Id);
                cmd.Parameters.AddWithValue("@Team_Name", (object?)dto.Team_Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Manager_Id", (object?)dto.Manager_Id ?? DBNull.Value);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) return NotFound("Team not found");
            }

            return Ok("Team updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("DELETE FROM Team WHERE Team_Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) return NotFound("Team not found");
            }

            return Ok("Team deleted");
        }
    }
}
