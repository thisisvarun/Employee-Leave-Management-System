using System.Data;
using backend.DTOs;
using Microsoft.Data.SqlClient;

namespace backend.Repository
{
    public class LoginRepository
    {
        private IConfiguration _configuration;
        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LoginDTO GetUserByEmail(LoginDTO loginDTO)
        {
            // Console.WriteLine(loginDTO.Email + " Password " + loginDTO.Password);
            string connectionString = _configuration.GetConnectionString("Default")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select Employee_Id, Email, PasswordHash FROM EMP.Employee WHERE EMP.Employee.Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Email", loginDTO.Email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoginDTO userLoginDBCredentials = new LoginDTO
                            {
                                EmployeeId = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                            };
                            return userLoginDBCredentials;
                        }
                    }
                }
            }
            return new LoginDTO();
        }
    }
}