using backend.Data.Interfaces;
using backend.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employees = new List<Employee>();
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM EMP.Employee";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                Employee_Id = reader.GetInt32(0),
                                First_Name = reader.GetString(1),
                                Last_Name = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Team_Id = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                                Salary = reader.GetInt32(6),
                                Designation_Id = reader.GetInt32(7),
                                Date_Of_Joining = reader.GetDateTime(8),
                                Active = reader.GetBoolean(9),
                                PasswordHash = reader.GetString(10),
                                // TODO: This should be from the DB as well!
                                Role = "Employee"
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = null!;
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM EMP.Employee WHERE Employee_Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                Employee_Id = reader.GetInt32(0),
                                First_Name = reader.GetString(1),
                                Last_Name = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Team_Id = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                                Salary = reader.GetInt32(6),
                                Designation_Id = reader.GetInt32(7),
                                Date_Of_Joining = reader.GetDateTime(8),
                                Active = reader.GetBoolean(9),
                                PasswordHash = reader.GetString(10),
                                Role = "Employee"
                            };
                        }
                    }
                }
            }
            return employee;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO EMP.Employee (First_Name, Last_Name, Email, Phone, Team_Id, Salary, Designation_Id, Date_Of_Joining, Active, PasswordHash) " +
                               "VALUES (@First_Name, @Last_Name, @Email, @Phone, @Team_Id, @Salary, @Designation_Id, @Date_Of_Joining, @Active, @PasswordHash); " +
                               "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@First_Name", employee.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", employee.Last_Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Team_Id", (object)employee.Team_Id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Designation_Id", employee.Designation_Id);
                    command.Parameters.AddWithValue("@Date_Of_Joining", employee.Date_Of_Joining);
                    command.Parameters.AddWithValue("@Active", employee.Active);
                    command.Parameters.AddWithValue("@PasswordHash", employee.PasswordHash);

                    employee.Employee_Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE EMP.Employee SET First_Name = @First_Name, Last_Name = @Last_Name, Email = @Email, Phone = @Phone, " +
                               "Team_Id = @Team_Id, Salary = @Salary, Designation_Id = @Designation_Id, Date_Of_Joining = @Date_Of_Joining, Active = @Active " +
                               "WHERE Employee_Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@First_Name", employee.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", employee.Last_Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Team_Id", (object)employee.Team_Id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Designation_Id", employee.Designation_Id);
                    command.Parameters.AddWithValue("@Date_Of_Joining", employee.Date_Of_Joining);
                    command.Parameters.AddWithValue("@Active", employee.Active);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM EMP.Employee WHERE Employee_Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}