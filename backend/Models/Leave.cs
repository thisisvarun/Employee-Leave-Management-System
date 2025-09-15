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
        public int LeaveRequest_Id { get; set; }
        public int Employee_Id { get; set; }
        public string Leave_Type { get; set; } = LeaveType.Casual;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = LeaveStatus.Pending;
        public string? Comment { get; set; }
    }
}
