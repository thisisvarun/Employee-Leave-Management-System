using backend.Data.Interfaces;
using backend.DTOs;
using backend.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Service
{
    public class HrService : IHrService
    {
        private readonly IHrRepository _hrRepository;
        private readonly IEmailService _emailService;

        public HrService(IHrRepository HrRepository, IEmailService emailService)
        {
            _hrRepository = HrRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<TeamLeaveRequestDto>> GetHrLeaveRequestsAsync(int hrId)
        {
            return await _hrRepository.GetHrLeaveRequestsAsync(hrId);
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, UpdateLeaveStatusDto dto)
        {
            var success = await _hrRepository.UpdateLeaveStatusAsync(leaveId, dto.Status, dto.Comment);
            if (success)
            {
                var employeeEmail = await _hrRepository.GetEmployeeEmailByLeaveIdAsync(leaveId);
                if (!string.IsNullOrEmpty(employeeEmail))
                {
                    string subject = $"Your leave request has been {dto.Status.ToLower()}.";
                    string body = $"Your leave request status has been updated to: {dto.Status}.\n\nHr's comment: {dto.Comment}";
                    await _emailService.SendEmailAsync(employeeEmail, subject, body);
                }
            }
            return success;
        }
    }
}