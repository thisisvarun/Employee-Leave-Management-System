using backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Service.Interfaces
{
    public interface IHrService
    {
        Task<IEnumerable<TeamLeaveRequestDto>> GetHrLeaveRequestsAsync(int managerId);
        Task<bool> UpdateLeaveStatusAsync(int leaveId, UpdateLeaveStatusDto dto);
    }
}
