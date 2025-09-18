using backend.DTOs;
using backend.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<EmployeeReadDTO> GetAllEmployees()
        {
            return _repository.GetAll();
        }

        public EmployeeReadDTO? GetEmployeeById(int employeeId)
        {
            return _repository.GetById(employeeId);
        }

        public void CreateEmployee(EmployeeCreateDTO dto)
        {
            string passwordHash = HashPassword(dto.Password);
            _repository.Create(dto, passwordHash);
        }

        public bool UpdateEmployee(EmployeeUpdateDTO dto)
        {
            return _repository.Update(dto);
        }

        public bool UpdatePassword(EmployeeUpdatePasswordDTO dto)
        {
            string? currentHash = _repository.GetPasswordHash(dto.Employee_Id);
            if (currentHash == null || !VerifyPassword(dto.OldPassword, currentHash))
                return false;

            string newHash = HashPassword(dto.NewPassword);
            return _repository.UpdatePassword(dto.Employee_Id, newHash);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return _repository.Delete(employeeId);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
