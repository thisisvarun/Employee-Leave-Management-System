// using backend.DTOs;
// using backend.Repositories;

// namespace backend.Services
// {
//     public class ApiService
//     {
//         private LoginRepository _apiRepository;

//         public ApiService(LoginRepository apiRepository)
//         {
//             _apiRepository = apiRepository;
//         }

//         public LoginDTO Login(LoginDTO loginDTO)
//         {
//             LoginDTO currentUserCredentials = _apiRepository.GetUserByEmail(loginDTO);
//             if (string.IsNullOrEmpty(currentUserCredentials.Email))
//             {
//                 return new LoginDTO { };
//             }

//             if (currentUserCredentials.Password != loginDTO.Password)
//             {
//                 return new LoginDTO { };
//             }
//             return currentUserCredentials;
//         }
//     }
// }