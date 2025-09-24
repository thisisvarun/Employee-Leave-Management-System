using backend.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class LeaveDto
    {
        public int EmployeeId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeaveType LeaveType { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<LeaveDateDto> Dates { get; set; } = new List<LeaveDateDto>();
    }

    public class LeaveHistoryDto
    {
        public int Request_Id { get; set; }
        public int EmployeeId { get; set; }
        public LeaveType LeaveType { get; set; } = LeaveType.Casual;
        public string Description { get; set; } = string.Empty;
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;
        public List<LeaveDateDto> Dates { get; set; } = new List<LeaveDateDto>();
    }
}
