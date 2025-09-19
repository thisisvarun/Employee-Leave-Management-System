using backend.Models;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class TeamRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public IEnumerable<TeamReadDTO> GetAll()
        {
            var teams = new List<TeamReadDTO>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using var cmd = new SqlCommand("SELECT * FROM Team", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teams.Add(MapTeam(reader));
                }
            }
            return teams;
        }

        public TeamReadDTO? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM Team WHERE Team_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapTeam(reader);
            return null;
        }

        public bool Add(Team team)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO Team (Team_Name, Manager_Id) VALUES (@Team_Name, @Manager_Id)", conn);
            cmd.Parameters.AddWithValue("@Team_Name", team.Team_Name);
            cmd.Parameters.AddWithValue("@Manager_Id", (object?)team.Manager_Id ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(Team team)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                "UPDATE Team SET Team_Name=@Team_Name, Manager_Id=@Manager_Id WHERE Team_Id=@Team_Id", conn);
            cmd.Parameters.AddWithValue("@Team_Id", team.Team_Id);
            cmd.Parameters.AddWithValue("@Team_Name", (object?)team.Team_Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Manager_Id", (object?)team.Manager_Id ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Team WHERE Team_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        private static TeamReadDTO MapTeam(SqlDataReader reader)
        {
            return new TeamReadDTO
            {
                Team_Id = reader.GetInt32(reader.GetOrdinal("Team_Id")),
                Team_Name = reader.GetString(reader.GetOrdinal("Team_Name")),
                Manager_Id = reader.IsDBNull(reader.GetOrdinal("Manager_Id"))
                                ? null
                                : reader.GetInt32(reader.GetOrdinal("Manager_Id"))
            };
        }
    }
}
