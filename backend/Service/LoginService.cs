using backend.Data.Interfaces;
using backend.DTOs;
using backend.Service.Interfaces;

namespace backend.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordService _passwordService;

        public LoginService(ILoginRepository loginRepository, IPasswordService passwordService)
        {
            _loginRepository = loginRepository;
            _passwordService = passwordService;
        }

        public LoginDTO Login(LoginDTO loginDTO)
        {
            LoginDTO currentUserCredentials = _loginRepository.GetUserDetails(loginDTO);
            if (string.IsNullOrEmpty(currentUserCredentials.Email))
            {
                return new LoginDTO { };
            }

            // if (!_passwordService.VerifyPassword(loginDTO.Password, currentUserCredentials.Password))
            // {
            //     return new LoginDTO { };
            // }
            return currentUserCredentials;
        }
    }
}
