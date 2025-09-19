using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class LoginRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public (string? PasswordHash, int EmployeeId)? GetPasswordHashByEmail(string email)
        {
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(@"
                SELECT Employee_Id, Password_Hash
                FROM Employee
                WHERE Email = @Email", conn);

            cmd.Parameters.AddWithValue("@Email", email);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return (
                    reader["Password_Hash"]?.ToString(),
                    reader.GetInt32(reader.GetOrdinal("Employee_Id"))
                );
            }

            return null;
        }
    }
}
