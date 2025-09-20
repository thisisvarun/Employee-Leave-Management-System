using backend.DTOs;

namespace backend.Data.Interfaces
{
    public interface ILoginRepository
    {
        LoginDTO GetUserByEmail(LoginDTO loginDTO);
    }
}
