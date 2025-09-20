using backend.Models.Enums;
using System.Collections.Generic;

namespace backend.DTOs
{
    public class LeaveDto
    {
        public int EmployeeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<LeaveDateDto> Dates { get; set; } = new List<LeaveDateDto>();
    }
}
