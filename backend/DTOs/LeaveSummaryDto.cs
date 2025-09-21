namespace backend.DTOs
{
    public class LeaveSummaryDto
    {
        public LeaveTypeSummary Casual { get; set; } = new LeaveTypeSummary();
        public LeaveTypeSummary Sick { get; set; } = new LeaveTypeSummary();
        public LeaveTypeSummary Annual { get; set; } = new LeaveTypeSummary();
        public LeaveTypeSummary Lieu { get; set; } = new LeaveTypeSummary();
    }
}
