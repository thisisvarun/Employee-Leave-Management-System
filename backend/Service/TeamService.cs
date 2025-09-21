using backend.Data.Interfaces;
using backend.DTOs;
using backend.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Service
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IEmailService _emailService;

        public TeamService(ITeamRepository teamRepository, IEmailService emailService)
        {
            _teamRepository = teamRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<TeamLeaveRequestDto>> GetTeamLeaveRequestsAsync(int managerId)
        {
            return await _teamRepository.GetTeamLeaveRequestsAsync(managerId);
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, UpdateLeaveStatusDto dto)
        {
            var success = await _teamRepository.UpdateLeaveStatusAsync(leaveId, dto.Status, dto.Comment);
            if (success)
            {
                var employeeEmail = await _teamRepository.GetEmployeeEmailByLeaveIdAsync(leaveId);
                if (!string.IsNullOrEmpty(employeeEmail))
                {
                    string subject = $"Your leave request has been {dto.Status.ToLower()}.";
                    string body = $"Your leave request status has been updated to: {dto.Status}.\n\nManager's comment: {dto.Comment}";
                    await _emailService.SendEmailAsync(employeeEmail, subject, body);
                }
            }
            return success;
        }
    }
}