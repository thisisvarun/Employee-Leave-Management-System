using backend.DTOs;

namespace backend.Service.Interfaces
{
    public interface IApiService
    {
        LoginDTO Login(LoginDTO loginDTO);
    }
}
