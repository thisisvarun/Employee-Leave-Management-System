using System.Data;
using backend.Data.Interfaces;
using backend.DTOs;
using backend.Models.Enums;
using Microsoft.Data.SqlClient;

namespace backend.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private IConfiguration _configuration;
        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LoginDTO GetUserByEmail(LoginDTO loginDTO)
        {
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT e.Employee_Id, e.Email, e.PasswordHash, d.Title, e.[Role] AS Role
                    FROM EMP.Employee e
                    JOIN EMP.Designation d ON e.Designation_Id = d.Designation_Id
                    WHERE e.Email = @Email;
                ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Email", loginDTO.Email);
                    Console.WriteLine(loginDTO.Email + " " + loginDTO.Password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("AM I HERE");
                            return new LoginDTO
                            {
                                EmployeeId = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                                RoleTitle = reader.GetString(3),
                                Role = reader.GetString(4) == "Employee" ? RoleType.Employee : RoleType.Manager
                            };
                        }
                    }
                }
            }
            return new LoginDTO();
        }
    }
}