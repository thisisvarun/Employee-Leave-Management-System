using backend.DTOs;
using backend.Repository;

namespace backend.Service
{
    public class ApiSevice
    {
        private LoginRepository _apiRepository;

        public ApiSevice(LoginRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public LoginDTO Login(LoginDTO loginDTO)
        {
            LoginDTO currentUserCredentials = _apiRepository.GetUserByEmail(loginDTO);
            if (string.IsNullOrEmpty(currentUserCredentials.Email))
            {
                return new LoginDTO { };
            }

            if (currentUserCredentials.Password != loginDTO.Password)
            {
                return new LoginDTO { };
            }
            return currentUserCredentials;
        }
    }
}