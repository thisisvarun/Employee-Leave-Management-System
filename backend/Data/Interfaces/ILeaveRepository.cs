using backend.DTOs;
using backend.Models;

namespace backend.Data.Interfaces
{
    public interface ILeaveRepository
    {
        Task<int> CreateLeaveAsync(Leave leave, List<Dates> dates);
        Task<bool> UpdateLeaveAsync(int leaveId, Leave leave, List<Dates> dates);
        Task<bool> DeleteLeaveAsync(int leaveId);
        Task<(string? Email, string? Name)> GetManagerInfoAsync(int employeeId);
        Task<LeaveSummaryDto> GetLeaveSummaryAsync(int employeeId);
        Task<Leave?> GetMostRecentProcessedLeaveAsync(int employeeId);
        Task<List<LeaveHistoryDto>> GetLeaveHistoryAsync(int employeeId);
    }
}
