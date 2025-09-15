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

        public string Login(LoginDTO loginDTO)
        {
            LoginDTO currentUserCredentials = _apiRepository.GetUserByEmail(loginDTO);
            if (string.IsNullOrEmpty(currentUserCredentials.Email))
            {
                return "The user does not exist!";
            }

            if (currentUserCredentials.Password != loginDTO.Password)
            {
                return "The password is incorrect!";
            }

            return "You've successfully logged in to our system!";
        }
    }
}