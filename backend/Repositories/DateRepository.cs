using backend.Models;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class DateRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public DateReadDTO Add(Dates date)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand(@"
                INSERT INTO Dates (Leave_Id, Hours, Date)
                OUTPUT INSERTED.Id
                VALUES (@Leave_Id, @Hours, @Date)", conn);

            cmd.Parameters.AddWithValue("@Leave_Id", date.Leave_Id);
            cmd.Parameters.AddWithValue("@Hours", date.Hours);
            cmd.Parameters.AddWithValue("@Date", date.Date);

            int newId = (int)cmd.ExecuteScalar()!;
            return new DateReadDTO
            {
                Id = newId,
                Leave_Id = date.Leave_Id,
                Hours = date.Hours,
                Date = date.Date
            };
        }

        public List<DateReadDTO> GetByLeaveId(int leaveId)
        {
            var dates = new List<DateReadDTO>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM Dates WHERE Leave_Id=@LeaveId", conn);
            cmd.Parameters.AddWithValue("@LeaveId", leaveId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dates.Add(new DateReadDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Leave_Id = reader.GetInt32(reader.GetOrdinal("Leave_Id")),
                    Hours = reader.GetInt32(reader.GetOrdinal("Hours")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date"))
                });
            }
            return dates;
        }

        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("DELETE FROM Dates WHERE Leave_Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
