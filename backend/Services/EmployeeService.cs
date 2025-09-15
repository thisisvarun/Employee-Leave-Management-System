using System.Data;
using System.Data.SqlClient;
using backend.Models;
using backend.DTOs;

namespace backend.Services
{
    public class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // CREATE Employee
        public EmployeeReadDTO CreateEmployee(EmployeeCreateDTO dto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Employee 
                    (First_Name, Last_Name, Email, Phone, Team_Id, Salary, Designation_Id, Date_Of_Joining, Is_Active, Password_Hash)
                    OUTPUT INSERTED.Employee_Id
                    VALUES (@First_Name, @Last_Name, @Email, @Phone, @Team_Id, @Salary, @Designation_Id, @Date_Of_Joining, @Is_Active, @Password_Hash)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@First_Name", dto.First_Name);
                    cmd.Parameters.AddWithValue("@Last_Name", dto.Last_Name);
                    cmd.Parameters.AddWithValue("@Email", dto.Email);
                    cmd.Parameters.AddWithValue("@Phone", dto.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Team_Id", dto.Team_Id);
                    cmd.Parameters.AddWithValue("@Salary", dto.Salary);
                    cmd.Parameters.AddWithValue("@Designation_Id", dto.Designation_Id);
                    cmd.Parameters.AddWithValue("@Date_Of_Joining", dto.Date_Of_Joining);
                    cmd.Parameters.AddWithValue("@Is_Active", dto.Is_Active);
                    cmd.Parameters.AddWithValue("@Password_Hash", HashPassword(dto.Password)); // implement HashPassword

                    int newId = (int)cmd.ExecuteScalar();

                    return GetEmployeeById(newId);
                }
            }
        }

        // READ by ID
        public EmployeeReadDTO? GetEmployeeById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Employee_Id, First_Name, Last_Name, Email, Phone, Team_Id, Salary, Designation_Id, Date_Of_Joining, Active FROM Employee WHERE Employee_Id = @Id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return null;

                        return new EmployeeReadDTO
                        {
                            Employee_Id = (int)reader["Employee_Id"],
                            FullName = $"{reader["First_Name"]} {reader["Last_Name"]}",
                            Email = reader["Email"].ToString()!,
                            Phone = reader["Phone"]?.ToString(),
                            Salary = (int)reader["Salary"],
                            Date_Of_Joining = (DateTime)reader["Date_Of_Joining"],
                            Is_Active = (bool)reader["Active"],
                            // Optional: fetch Team and Designation names if needed
                        };
                    }
                }
            }
        }

        // READ all
        public List<EmployeeReadDTO> GetAllEmployees()
        {
            var employees = new List<EmployeeReadDTO>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Employee_Id, First_Name, Last_Name, Email, Phone, Team_Id, Salary, Designation_Id, Date_Of_Joining, Active FROM Employee";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeReadDTO
                        {
                            Employee_Id = (int)reader["Employee_Id"],
                            FullName = $"{reader["First_Name"]} {reader["Last_Name"]}",
                            Email = reader["Email"].ToString()!,
                            Phone = reader["Phone"]?.ToString(),
                            Salary = (int)reader["Salary"],
                            Date_Of_Joining = (DateTime)reader["Date_Of_Joining"],
                            Is_Active = (bool)reader["Active"],
                        });
                    }
                }
            }

            return employees;
        }

        // UPDATE
        public bool UpdateEmployee(EmployeeUpdateDTO dto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Employee SET
                        Phone = @Phone,
                        Team_Id = @Team_Id,
                        Salary = @Salary,
                        Designation_Id = @Designation_Id,
                        Active = @Active
                    WHERE Employee_Id = @Employee_Id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_Id", dto.Employee_Id);
                    cmd.Parameters.AddWithValue("@Phone", dto.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Team_Id", dto.Team_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Salary", dto.Salary ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Designation_Id", dto.Designation_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Active", dto.Is_Active ?? (object)DBNull.Value);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // DELETE
        public bool DeleteEmployee(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employee WHERE Employee_Id = @Id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // UPDATE PASSWORD
        public bool UpdatePassword(EmployeeUpdatePasswordDTO dto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT Password_Hash FROM Employee WHERE Employee_Id = @Id";
                using (var selectCmd = new SqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@Id", dto.Employee_Id);
                    var currentHash = selectCmd.ExecuteScalar()?.ToString();
                    if (currentHash == null || !VerifyPassword(dto.OldPassword, currentHash))
                        return false;
                }

                string updateQuery = "UPDATE Employee SET Password_Hash = @NewHash WHERE Employee_Id = @Id";
                using (var updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@Id", dto.Employee_Id);
                    updateCmd.Parameters.AddWithValue("@NewHash", HashPassword(dto.NewPassword));
                    return updateCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Example password hashing / verification (replace with BCrypt for production)
        private string HashPassword(string password)
        {
            return password; // placeholder, replace with real hashing
        }

        private bool VerifyPassword(string password, string hash)
        {
            return password == hash; // placeholder, replace with hash verification
        }
    }
}
