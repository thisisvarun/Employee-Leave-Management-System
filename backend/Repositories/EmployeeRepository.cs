using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class EmployeeRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<EmployeeReadDTO> GetAll()
        {
            var employees = new List<EmployeeReadDTO>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT e.Employee_Id, e.First_Name, e.Last_Name, e.Email, e.Phone, e.Salary, e.Date_Of_Joining, e.Active,
                       d.Designation_Name, t.Team_Name
                FROM Employee e
                JOIN Designation d ON e.Designation_Id = d.Designation_Id
                LEFT JOIN Team t ON e.Team_Id = t.Team_Id", conn); // LEFT JOIN in case Team_Id is null

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(MapEmployee(reader));
            }

            return employees;
        }

        public EmployeeReadDTO? GetById(int employeeId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT e.Employee_Id, e.First_Name, e.Last_Name, e.Email, e.Phone, e.Salary, e.Date_Of_Joining, e.Active,
                       d.Designation_Name, t.Team_Name
                FROM Employee e
                JOIN Designation d ON e.Designation_Id = d.Designation_Id
                LEFT JOIN Team t ON e.Team_Id = t.Team_Id
                WHERE e.Employee_Id=@EmployeeId", conn);

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapEmployee(reader);

            return null;
        }

        public int Create(EmployeeCreateDTO dto, string passwordHash)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO Employee (First_Name, Last_Name, Email, Phone, Team_Id, Salary, Designation_Id, Date_Of_Joining, Active, Password_Hash)
                OUTPUT INSERTED.Employee_Id
                VALUES (@First_Name, @Last_Name, @Email, @Phone, @Team_Id, @Salary, @Designation_Id, @Date_Of_Joining, @Active, @Password_Hash)", conn);

            cmd.Parameters.AddWithValue("@First_Name", dto.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", dto.Last_Name);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Phone", dto.Phone);
            cmd.Parameters.AddWithValue("@Team_Id", (object?)dto.Team_Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Salary", dto.Salary);
            cmd.Parameters.AddWithValue("@Designation_Id", dto.Designation_Id);
            cmd.Parameters.AddWithValue("@Date_Of_Joining", dto.Date_Of_Joining);
            cmd.Parameters.AddWithValue("@Active", dto.Is_Active);
            cmd.Parameters.AddWithValue("@Password_Hash", passwordHash);

            return (int)cmd.ExecuteScalar(); // Returns new Employee_Id
        }

        public bool Update(EmployeeUpdateDTO dto)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                UPDATE Employee
                SET Phone=@Phone, Team_Id=@Team_Id, Salary=@Salary, Designation_Id=@Designation_Id, Active=@Active
                WHERE Employee_Id=@Employee_Id", conn);

            cmd.Parameters.AddWithValue("@Employee_Id", dto.Employee_Id);
            cmd.Parameters.AddWithValue("@Phone", (object?)dto.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Team_Id", (object?)dto.Team_Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Salary", (object?)dto.Salary ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Designation_Id", (object?)dto.Designation_Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Active", (object?)dto.Is_Active ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        public string? GetPasswordHash(int employeeId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("SELECT Password_Hash FROM Employee WHERE Employee_Id=@EmployeeId", conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            return cmd.ExecuteScalar()?.ToString();
        }

        public bool UpdatePassword(int employeeId, string newHash)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("UPDATE Employee SET Password_Hash=@NewHash WHERE Employee_Id=@EmployeeId", conn);
            cmd.Parameters.AddWithValue("@NewHash", newHash);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int employeeId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM Employee WHERE Employee_Id=@EmployeeId", conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            return cmd.ExecuteNonQuery() > 0;
        }

        private static EmployeeReadDTO MapEmployee(SqlDataReader reader)
        {
            string? teamName = reader["Team_Name"]?.ToString();
            return new EmployeeReadDTO
            {
                Employee_Id = reader.GetInt32(reader.GetOrdinal("Employee_Id")),
                First_Name = reader["First_Name"]?.ToString() ?? string.Empty,
                Last_Name = reader["Last_Name"]?.ToString() ?? string.Empty,
                Email = reader["Email"]?.ToString() ?? string.Empty,
                Phone = reader["Phone"]?.ToString() ?? string.Empty,
                Designation = reader["Designation_Name"]?.ToString() ?? string.Empty,
                Team = string.IsNullOrWhiteSpace(teamName) ? "On Bench" : teamName,
                Salary = reader.GetInt32(reader.GetOrdinal("Salary")),
                Date_Of_Joining = reader.GetDateTime(reader.GetOrdinal("Date_Of_Joining")),
                Is_Active = reader.GetBoolean(reader.GetOrdinal("Active"))
            };
        }
    }
}
