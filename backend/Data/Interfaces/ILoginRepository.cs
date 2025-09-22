using backend.DTOs;

namespace backend.Data.Interfaces
{
    public interface ILoginRepository
    {
        LoginDTO GetUserDetails(LoginDTO loginDTO);
    }
}
