using backend.Data.Interfaces;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repository
{
    public class HrRepository : IHrRepository
    {
        private readonly IConfiguration _configuration;

        public HrRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TeamLeaveRequestDto>> GetHrLeaveRequestsAsync(int hrId)
        {
            Console.WriteLine($"[HrRepository] Fetching leave requests for HrId: {hrId}");
            var leaveRequestsMap = new Dictionary<int, TeamLeaveRequestDto>();
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
                        l.Description,
                        d.Date,
                        d.Hours
                    FROM LEAVES.Leave l
                    JOIN EMP.Employee e ON l.Employee_Id = e.Employee_Id
                    LEFT JOIN LEAVES.Dates d ON l.LeaveRequest_Id = d.Leave_Id
                    WHERE l.Status = 'Pending';
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int leaveRequestId = reader.GetInt32(0);
                            if (!leaveRequestsMap.TryGetValue(leaveRequestId, out var dto))
                            {
                                dto = new TeamLeaveRequestDto
                                {
                                    LeaveRequestId = leaveRequestId,
                                    EmployeeId = reader.GetInt32(1),
                                    EmployeeName = reader.GetString(2),
                                    LeaveType = reader.GetString(3),
                                    Status = reader.GetString(4),
                                    Description = reader.GetString(5),
                                    Dates = new List<LeaveDateDto>()
                                };
                                leaveRequestsMap.Add(leaveRequestId, dto);
                            }

                            if (!reader.IsDBNull(6)) // Check if Date is not null
                            {
                                dto.Dates.Add(new LeaveDateDto
                                {
                                    Date = reader.GetDateTime(6),
                                    Hours = reader.GetInt32(7)
                                });
                            }
                        }
                    }
                }
            }
            Console.WriteLine("[Hr REPO]", leaveRequestsMap.Values);
            return leaveRequestsMap.Values;
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
