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
        [Key]
        public int LeaveRequestId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string? Comment { get; set; }
        public List<Dates> Dates { get; set; } = [];
    }
}
