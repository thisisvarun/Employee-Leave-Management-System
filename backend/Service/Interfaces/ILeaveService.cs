using backend.DTOs;
using backend.Models;

namespace backend.Service.Interfaces
{
    public interface ILeaveService
    {
        Task<int> ApplyLeaveAsync(LeaveDto leaveDto);
        Task<bool> UpdateLeaveAsync(int leaveId, LeaveDto leaveDto);
        Task<bool> CancelLeaveAsync(int leaveId);
        Task<LeaveSummaryDto> GetLeaveSummaryAsync(int employeeId);
        Task<Leave?> GetMostRecentProcessedLeaveAsync(int employeeId);
        Task<List<LeaveHistoryDto>> GetLeaveHistoryAsync(int employeeId);
    }
}
