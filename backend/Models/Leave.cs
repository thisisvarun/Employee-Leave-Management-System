<<<<<<< HEAD
﻿namespace backend.Models
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

=======
﻿using System;

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
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
    public class Leave
    {
        public int LeaveRequest_Id { get; set; }
        public int Employee_Id { get; set; }
        public LeaveType Leave_Type { get; set; } = LeaveType.Casual;
        public string Description { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string? Comment { get; set; }
        public List<Dates> Dates { get; set; } = [];
    }
}
