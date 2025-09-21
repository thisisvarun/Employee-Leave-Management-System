using backend.Data.Interfaces;
using backend.DTOs;
using backend.Models;
using backend.Models.Enums;
using Microsoft.Data.SqlClient;

namespace backend.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IConfiguration _configuration;

        public LeaveRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateLeaveAsync(Leave leave, List<Dates> dates)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string leaveQuery = "INSERT INTO LEAVES.Leave (Employee_Id, Leave_Type, [Description], [Status], Comment) " +
                                          "VALUES (@Employee_Id, @Leave_Type, @Description, @Status, @Comment); " +
                                          "SELECT SCOPE_IDENTITY();";
                        int leaveId;
                        using (SqlCommand command = new SqlCommand(leaveQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Employee_Id", leave.EmployeeId);
                            command.Parameters.AddWithValue("@Leave_Type", leave.LeaveType.ToString());
                            command.Parameters.AddWithValue("@Description", leave.Description);
                            command.Parameters.AddWithValue("@Status", leave.Status);
                            command.Parameters.AddWithValue("@Comment", leave.Comment);
                            leaveId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        }

                        foreach (var date in dates)
                        {
                            string dateQuery = "INSERT INTO LEAVES.Dates (Leave_Id, [Hours], [Date]) VALUES (@Leave_Id, @Hours, @Date)";
                            using (SqlCommand dateCommand = new SqlCommand(dateQuery, connection, transaction))
                            {
                                dateCommand.Parameters.AddWithValue("@Leave_Id", leaveId);
                                dateCommand.Parameters.AddWithValue("@Hours", date.Hours);
                                dateCommand.Parameters.AddWithValue("@Date", date.Date);
                                await dateCommand.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                        return leaveId;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> UpdateLeaveAsync(int leaveId, Leave leave, List<Dates> dates)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string leaveQuery = "UPDATE LEAVES.Leave SET Leave_Type = @Leave_Type, [Description] = @Description, [Status] = @Status, Comment = @Comment " +
                                          "WHERE LeaveRequest_Id = @LeaveRequest_Id";
                        using (SqlCommand command = new SqlCommand(leaveQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@LeaveRequest_Id", leaveId);
                            command.Parameters.AddWithValue("@Leave_Type", leave.LeaveType.ToString());
                            command.Parameters.AddWithValue("@Description", leave.Description);
                            command.Parameters.AddWithValue("@Status", leave.Status);
                            command.Parameters.AddWithValue("@Comment", leave.Comment);
                            await command.ExecuteNonQueryAsync();
                        }

                        string deleteDatesQuery = "DELETE FROM LEAVES.Dates WHERE Leave_Id = @Leave_Id";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteDatesQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@Leave_Id", leaveId);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }

                        foreach (var date in dates)
                        {
                            string dateQuery = "INSERT INTO LEAVES.Dates (Leave_Id, [Hours], [Date]) VALUES (@Leave_Id, @Hours, @Date)";
                            using (SqlCommand dateCommand = new SqlCommand(dateQuery, connection, transaction))
                            {
                                dateCommand.Parameters.AddWithValue("@Leave_Id", leaveId);
                                dateCommand.Parameters.AddWithValue("@Hours", date.Hours);
                                dateCommand.Parameters.AddWithValue("@Date", date.Date);
                                await dateCommand.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> DeleteLeaveAsync(int leaveId)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteDatesQuery = "DELETE FROM LEAVES.Dates WHERE Leave_Id = @Leave_Id";
                        using (SqlCommand deleteDatesCommand = new SqlCommand(deleteDatesQuery, connection, transaction))
                        {
                            deleteDatesCommand.Parameters.AddWithValue("@Leave_Id", leaveId);
                            await deleteDatesCommand.ExecuteNonQueryAsync();
                        }

                        string deleteLeaveQuery = "DELETE FROM LEAVES.Leave WHERE LeaveRequest_Id = @LeaveRequest_Id";
                        using (SqlCommand deleteLeaveCommand = new SqlCommand(deleteLeaveQuery, connection, transaction))
                        {
                            deleteLeaveCommand.Parameters.AddWithValue("@LeaveRequest_Id", leaveId);
                            int rowsAffected = await deleteLeaveCommand.ExecuteNonQueryAsync();
                            await transaction.CommitAsync();
                            return rowsAffected > 0;
                        }
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
        }

        public async Task<(string? Email, string? Name)> GetManagerInfoAsync(int employeeId)
        {
            (string? Email, string? Name) managerInfo = (null, null);
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT m.Email, m.First_Name + ' ' + m.Last_Name AS ManagerName
                    FROM EMP.Employee e
                    JOIN EMP.Team t ON e.Team_Id = t.Team_Id
                    JOIN EMP.Employee m ON t.Manager_Id = m.Employee_Id
                    WHERE e.Employee_Id = @EmployeeId;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            managerInfo = (reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }
            return managerInfo;
        }

        public async Task<LeaveSummaryDto> GetLeaveSummaryAsync(int employeeId)
        {
            var summary = new LeaveSummaryDto();
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT
                        l.Leave_Type,
                        l.Status,
                        COUNT(d.Leave_Id) AS LeaveDayCount
                    FROM LEAVES.Leave l
                    JOIN LEAVES.Dates d ON l.LeaveRequest_Id = d.Leave_Id
                    WHERE l.Employee_Id = @EmployeeId AND l.Status IN ('Approved', 'Pending')
                    GROUP BY l.Leave_Type, l.Status;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string leaveType = reader.GetString(0);
                            string status = reader.GetString(1);
                            int count = reader.GetInt32(2);

                            LeaveTypeSummary? summaryToUpdate = null;
                            switch (leaveType)
                            {
                                case "Casual": summaryToUpdate = summary.Casual; break;
                                case "Sick": summaryToUpdate = summary.Sick; break;
                                case "Annual": summaryToUpdate = summary.Annual; break;
                                case "LIEU": summaryToUpdate = summary.Lieu; break;
                            }

                            if (summaryToUpdate != null)
                            {
                                if (status == "Approved") summaryToUpdate.Approved = count;
                                else if (status == "Pending") summaryToUpdate.Pending = count;
                            }
                        }
                    }
                }
            }
            return summary;
        }
    }
}
