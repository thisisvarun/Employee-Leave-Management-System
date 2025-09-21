using backend.Data.Interfaces;
using backend.DTOs;
using backend.Models;
using backend.Service.Interfaces;

namespace backend.Service
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailService _emailService;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository, IEmailService emailService)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _emailService = emailService;
        }

        public async Task<int> ApplyLeaveAsync(LeaveDto leaveDto)
        {
            var summary = await GetLeaveSummaryAsync(leaveDto.EmployeeId);
            var requestedDays = leaveDto.Dates.Count;

            LeaveTypeSummary? relevantSummary = null;
            switch (leaveDto.LeaveType)
            {
                case Models.Enums.LeaveType.Casual: relevantSummary = summary.Casual; break;
                case Models.Enums.LeaveType.Sick: relevantSummary = summary.Sick; break;
                case Models.Enums.LeaveType.Annual: relevantSummary = summary.Annual; break;
                case Models.Enums.LeaveType.LIEU: relevantSummary = summary.Lieu; break;
            }

            if (relevantSummary != null)
            {
                if ((relevantSummary.Total - relevantSummary.Approved - relevantSummary.Pending) < requestedDays)
                {
                    return -1; // Indicates insufficient balance
                }
            }

            var leave = new Leave
            {
                EmployeeId = leaveDto.EmployeeId,
                LeaveType = leaveDto.LeaveType,
                Description = leaveDto.Description,
                Status = "Pending"
            };

            var dates = leaveDto.Dates.Select(d => new Dates { Hours = d.Hours, Date = d.Date }).ToList();

            int leaveId = await _leaveRepository.CreateLeaveAsync(leave, dates);

            var employee = await _employeeRepository.GetEmployeeByIdAsync(leaveDto.EmployeeId);
            var managerInfo = await _leaveRepository.GetManagerInfoAsync(leaveDto.EmployeeId);

            if (employee != null)
            {
                string employeeSubject = "Leave Application Submitted Successfully";
                string employeeBody = $"Dear {employee.First_Name},<br><br>"
                                    + "This email confirms that your leave application has been successfully submitted.<br><br>"
                                    + "<b>Leave Details:</b><br>"
                                    + $"- Type: {leave.LeaveType}<br>"
                                    + $"- Dates: {leaveDto.Dates.FirstOrDefault()?.Date:dd MMM yyyy} to {leaveDto.Dates.LastOrDefault()?.Date:dd MMM yyyy}<br>"
                                    + $"- Reason: {leave.Description}<br><br>"
                                    + "Your application is now pending approval from your manager. You will be notified once its status is updated.<br><br>"
                                    + "Thank you,<br><b>The HR Team</b>";

                await _emailService.SendEmailAsync(employee.Email, employeeSubject, employeeBody);
            }

            if (managerInfo.Email != null)
            {
                string managerSubject = "New Leave Application for Review";
                string managerBody = $"Dear {managerInfo.Name},<br><br>"
                                   + $"A new leave application has been submitted by {employee?.First_Name} {employee?.Last_Name} and requires your review.<br><br>"
                                   + "<b>Application Details:</b><br>"
                                   + $"- Employee: {employee?.First_Name} {employee?.Last_Name}<br>"
                                   + $"- Type: {leave.LeaveType}<br>"
                                   + $"- Dates: {leaveDto.Dates.FirstOrDefault()?.Date:dd MMM yyyy} to {leaveDto.Dates.LastOrDefault()?.Date:dd MMM yyyy}<br>"
                                   + $"- Reason: {leave.Description}<br><br>"
                                   + "Please log in to the employee management portal to approve or reject this request.<br><br>"
                                   + "Thank you,<br><b>The HR Team</b>";

                await _emailService.SendEmailAsync(managerInfo.Email, managerSubject, managerBody);
            }
            return leaveId;
        }

        public async Task<bool> UpdateLeaveAsync(int leaveId, LeaveDto leaveDto)
        {
            var leave = new Leave
            {
                EmployeeId = leaveDto.EmployeeId,
                LeaveType = leaveDto.LeaveType,
                Description = leaveDto.Description,
                Status = "Pending"
            };

            var dates = leaveDto.Dates.Select(d => new Dates { Hours = d.Hours, Date = d.Date }).ToList();

            return await _leaveRepository.UpdateLeaveAsync(leaveId, leave, dates);
        }

        public async Task<bool> CancelLeaveAsync(int leaveId)
        {
            return await _leaveRepository.DeleteLeaveAsync(leaveId);
        }

        public async Task<LeaveSummaryDto> GetLeaveSummaryAsync(int employeeId)
        {
            var summary = await _leaveRepository.GetLeaveSummaryAsync(employeeId);
            summary.Casual.Total = 7;
            summary.Sick.Total = 7;
            summary.Annual.Total = 15;
            summary.Lieu.Total = 10;
            return summary;
        }
    }
}
