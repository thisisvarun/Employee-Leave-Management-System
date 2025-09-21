using System;
using System.Collections.Generic;

namespace backend.DTOs
{
    public class TeamLeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<LeaveDateDto> Dates { get; set; } = new List<LeaveDateDto>();
    }
}
