using backend.Models.Enums;
using System;

namespace backend.Models
{
    public enum LeaveType
    {
        Casual, Sick, Annual, LIEU
    }
    public enum LeaveStatus
    {
        Approved, Rejected, Pending
    }
    public class Leave
    {
        public int LeaveRequestId { get; set; }
        public int Employee_Id { get; set; }
        public LeaveType Leave_Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string? Comment { get; set; }
        public List<Dates> Dates { get; set; } = [];
    }
}
