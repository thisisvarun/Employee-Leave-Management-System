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
            _apiRepository.GetUserByUsername(loginDTO.Email);
            return "Done";
        }
    }
}