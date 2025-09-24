using backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Data.Interfaces
{
    public interface IHrRepository
    {
        Task<IEnumerable<TeamLeaveRequestDto>> GetHrLeaveRequestsAsync(int hrId);
        Task<bool> UpdateLeaveStatusAsync(int leaveId, string status, string comment);
        Task<string?> GetEmployeeEmailByLeaveIdAsync(int leaveId);
    }
}
