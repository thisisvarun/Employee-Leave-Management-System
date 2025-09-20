using backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Service.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamLeaveRequestDto>> GetTeamLeaveRequestsAsync(int managerId);
        Task<bool> UpdateLeaveStatusAsync(int leaveId, UpdateLeaveStatusDto dto);
    }
}
