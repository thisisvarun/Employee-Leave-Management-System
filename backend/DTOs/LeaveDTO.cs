using System;

namespace backend.DTOs
{
    public class LeaveCreateDTO
    {
        public int Employee_Id { get; set; }
        public string Leave_Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class LeaveReadDTO
    {
        public int LeaveRequest_Id { get; set; }
        public int Employee_Id { get; set; }
        public string Leave_Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class LeaveUpdateDTO
    {
        public int LeaveRequest_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
    }

    public class LeaveDeleteDTO
    {
        public int LeaveRequest_Id { get; set; }
    }
}