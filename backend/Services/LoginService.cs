using backend.DTOs;
using backend.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services
{
    public class LoginService(LoginRepository repository)
    {
        private readonly LoginRepository _repository = repository;

        public int? Login(LoginDTO loginDto)
        {
            var dbResult = _repository.GetPasswordHashByEmail(loginDto.Email);
            if (dbResult == null) return null;

            string dbHash = dbResult.Value.PasswordHash ?? string.Empty;
            string inputHash = HashPassword(loginDto.Password);

            if (dbHash != inputHash) return null;

            return dbResult.Value.EmployeeId; // returns employee id if login successful
        }

        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}
