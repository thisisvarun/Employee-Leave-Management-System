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
            var managerEmail = await _leaveRepository.GetManagerEmailAsync(leaveDto.EmployeeId);

            string subject = "Leave Application Submitted";
            string body = $"Your leave application has been submitted successfully.";

            if (employee != null)
            {
                await _emailService.SendEmailAsync(employee.Email, subject, body);
            }

            if (!string.IsNullOrEmpty(managerEmail))
            {
                await _emailService.SendEmailAsync(managerEmail, subject, $"A new leave application has been submitted by {employee?.First_Name} {employee?.Last_Name}.");
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
    }
}
