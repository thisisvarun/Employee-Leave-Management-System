namespace backend.Models
{
    public enum LeaveType
    {
        Casual,
        Sick,
        Annual,
        Lieu
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Leave
    {
        public int LeaveRequest_Id { get; set; }
        public int Employee_Id { get; set; }
        public LeaveType Leave_Type { get; set; } = LeaveType.Casual;
        public string Description { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string Comment { get; set; } = string.Empty;
    }
}
