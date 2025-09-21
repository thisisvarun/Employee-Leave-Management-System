using backend.Models;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class LeaveRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        // Get all leaves
        public List<Leave> GetAll()
        {
            var leaves = new List<Leave>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM LeaveRequests", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                leaves.Add(MapToLeave(reader));

            return leaves;
        }

        // Get leave by ID
        public Leave? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM LeaveRequests WHERE LeaveRequest_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapToLeave(reader);

            return null;
        }

        // Add leave and return new ID
        public int Add(Leave leave)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand(@"
                INSERT INTO LeaveRequests (Employee_Id, Leave_Type, Description, Status, Comment)
                OUTPUT INSERTED.LeaveRequest_Id
                VALUES (@Employee_Id, @Leave_Type, @Description, @Status, @Comment)", conn);

            cmd.Parameters.AddWithValue("@Employee_Id", leave.Employee_Id);
            cmd.Parameters.AddWithValue("@Leave_Type", leave.Leave_Type.ToString());
            cmd.Parameters.AddWithValue("@Description", leave.Description);
            cmd.Parameters.AddWithValue("@Status", leave.Status.ToString());
            cmd.Parameters.AddWithValue("@Comment", (object?)leave.Comment ?? DBNull.Value);

            return (int)cmd.ExecuteScalar()!;
        }

        // Update leave
        public bool Update(Leave leave)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand(@"
                UPDATE LeaveRequests
                SET Description=@Description, Status=@Status, Comment=@Comment
                WHERE LeaveRequest_Id=@LeaveRequest_Id", conn);

            cmd.Parameters.AddWithValue("@LeaveRequest_Id", leave.LeaveRequest_Id);
            cmd.Parameters.AddWithValue("@Description", leave.Description);
            cmd.Parameters.AddWithValue("@Status", leave.Status.ToString());
            cmd.Parameters.AddWithValue("@Comment", (object?)leave.Comment ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Delete leave
        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            // Delete associated dates first
            using (var cmdDates = new SqlCommand("DELETE FROM Dates WHERE Leave_Id=@Leave_Id", conn))
            {
                cmdDates.Parameters.AddWithValue("@Leave_Id", id);
                cmdDates.ExecuteNonQuery();
            }

            using var cmd = new SqlCommand("DELETE FROM LeaveRequests WHERE LeaveRequest_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Get dates by leave ID
        public List<DateReadDTO> GetDatesByLeaveId(int leaveId)
        {
            var dates = new List<DateReadDTO>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM Dates WHERE Leave_Id=@Leave_Id ORDER BY Date", conn);
            cmd.Parameters.AddWithValue("@Leave_Id", leaveId);

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

        // Helper: Map Leave
        private static Leave MapToLeave(SqlDataReader reader)
        {
            return new Leave
            {
                LeaveRequest_Id = reader.GetInt32(reader.GetOrdinal("LeaveRequest_Id")),
                Employee_Id = reader.GetInt32(reader.GetOrdinal("Employee_Id")),
                Leave_Type = Enum.Parse<LeaveType>(reader.GetString(reader.GetOrdinal("Leave_Type"))),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Status = Enum.Parse<LeaveStatus>(reader.GetString(reader.GetOrdinal("Status"))),
                Comment = reader.IsDBNull(reader.GetOrdinal("Comment")) ? null : reader.GetString(reader.GetOrdinal("Comment"))
            };
        }
    }
}
