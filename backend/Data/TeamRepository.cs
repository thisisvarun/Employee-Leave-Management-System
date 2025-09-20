using backend.Data.Interfaces;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IConfiguration _configuration;

        public TeamRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TeamLeaveRequestDto>> GetTeamLeaveRequestsAsync(int managerId)
        {
            var leaveRequests = new List<TeamLeaveRequestDto>();
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT 
                        l.LeaveRequest_Id, 
                        e.Employee_Id, 
                        e.First_Name + ' ' + e.Last_Name AS EmployeeName,
                        l.Leave_Type, 
                        l.Status,
                        l.Description
                    FROM LEAVES.Leave l
                    JOIN EMP.Employee e ON l.Employee_Id = e.Employee_Id
                    JOIN EMP.Team t ON e.Team_Id = t.Team_Id
                    WHERE t.Manager_Id = @ManagerId;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ManagerId", managerId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            leaveRequests.Add(new TeamLeaveRequestDto
                            {
                                LeaveRequestId = reader.GetInt32(0),
                                EmployeeId = reader.GetInt32(1),
                                EmployeeName = reader.GetString(2),
                                LeaveType = reader.GetString(3),
                                Status = reader.GetString(4),
                                Description = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return leaveRequests;
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, string status, string comment)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE LEAVES.Leave SET [Status] = @Status, Comment = @Comment WHERE LeaveRequest_Id = @LeaveId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Comment", comment);
                    command.Parameters.AddWithValue("@LeaveId", leaveId);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<string?> GetEmployeeEmailByLeaveIdAsync(int leaveId)
        {
            string? email = null;
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT e.Email
                    FROM LEAVES.Leave l
                    JOIN EMP.Employee e ON l.Employee_Id = e.Employee_Id
                    WHERE l.LeaveRequest_Id = @LeaveId;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaveId", leaveId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            email = reader.GetString(0);
                        }
                    }
                }
            }
            return email;
        }
    }
}
