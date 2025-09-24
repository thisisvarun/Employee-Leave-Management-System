using backend.DTOs;

namespace backend.Service.Interfaces
{
    public interface ILoginService
    {
        LoginDTO Login(LoginDTO loginDTO);
    }
}
