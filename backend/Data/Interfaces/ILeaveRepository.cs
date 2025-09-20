using backend.Models;

namespace backend.Data.Interfaces
{
    public interface ILeaveRepository
    {
        Task<int> CreateLeaveAsync(Leave leave, List<Dates> dates);
        Task<bool> UpdateLeaveAsync(int leaveId, Leave leave, List<Dates> dates);
        Task<bool> DeleteLeaveAsync(int leaveId);
        Task<string?> GetManagerEmailAsync(int employeeId);
    }
}
