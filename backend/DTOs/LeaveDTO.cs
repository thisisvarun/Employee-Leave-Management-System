using System;
using backend.Models;

namespace backend.DTOs
{
    public class LeaveCreateWithDatesDTO
    {
        public int Employee_Id { get; set; }
        public LeaveType Leave_Type { get; set; } = LeaveType.Casual;
        public string Description { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public List<DateCreateDTO> Dates { get; set; } = new();
    }

    public class LeaveReadDTO
    {
        public int LeaveRequest_Id { get; set; }
        public int Employee_Id { get; set; }
        public LeaveType Leave_Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string? Comment { get; set; }
        public List<DateReadDTO> Dates { get; set; } = [];
    }

    public class LeaveUpdateDTO
    {
        public int LeaveRequest_Id { get; set; }
        public string? Description { get; set; }
        public LeaveStatus? Status { get; set; } // nullable for partial update
        public string? Comment { get; set; }
    }

    public class LeaveDeleteDTO
    {
        public int LeaveRequest_Id { get; set; }
    }
}