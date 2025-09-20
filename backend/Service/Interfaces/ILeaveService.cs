using backend.DTOs;

namespace backend.Service.Interfaces
{
    public interface ILeaveService
    {
        Task<int> ApplyLeaveAsync(LeaveDto leaveDto);
        Task<bool> UpdateLeaveAsync(int leaveId, LeaveDto leaveDto);
        Task<bool> CancelLeaveAsync(int leaveId);
    }
}
