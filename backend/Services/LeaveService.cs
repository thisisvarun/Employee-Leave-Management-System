using backend.DTOs;
using backend.Models;
using backend.Repositories;
using System.Collections.Generic;

namespace backend.Services
{
    public class LeaveService(LeaveRepository leaveRepo, DateRepository dateRepo)
    {
        private readonly LeaveRepository _leaveRepo = leaveRepo;
        private readonly DateRepository _dateRepo = dateRepo;

        // Get all leave requests
        public List<LeaveReadDTO> GetAll()
        {
            var leaves = _leaveRepo.GetAll();
            var result = new List<LeaveReadDTO>();

            foreach (var leave in leaves)
            {
                var leaveDto = MapToReadDTO(leave);
                leaveDto.Dates = _dateRepo.GetByLeaveId(leave.LeaveRequest_Id);
                result.Add(leaveDto);
            }

            return result;
        }

        // Get leave by ID
        public LeaveReadDTO? GetById(int leaveRequestId)
        {
            var leave = _leaveRepo.GetById(leaveRequestId);
            if (leave == null) return null;

            var leaveDto = MapToReadDTO(leave);
            leaveDto.Dates = _dateRepo.GetByLeaveId(leave.LeaveRequest_Id);
            return leaveDto;
        }

        // Create leave request with dates
        public int Create(LeaveCreateWithDatesDTO dto)
        {
            var leave = new Leave
            {
                Employee_Id = dto.Employee_Id,
                Leave_Type = dto.Leave_Type,
                Description = dto.Description,
                Comment = dto.Comment,
                Status = LeaveStatus.Pending
            };

            // Add leave and get new LeaveRequest_Id
            int leaveId = _leaveRepo.Add(leave);

            // Add all associated dates
            foreach (var dateDto in dto.Dates)
            {
                _dateRepo.Add(new Dates
                {
                    Leave_Id = leaveId,
                    Hours = dateDto.Hours,
                    Date = dateDto.Date
                });
            }

            return leaveId;
        }

        // Update leave request
        public bool Update(LeaveUpdateDTO dto)
        {
            var leave = new Leave
            {
                LeaveRequest_Id = dto.LeaveRequest_Id,
                Description = dto.Description ?? string.Empty,
                Comment = dto.Comment,
                Status = dto.Status ?? LeaveStatus.Pending
            };

            return _leaveRepo.Update(leave);
        }

        // Delete leave request (and optionally its dates)
        public bool Delete(int leaveRequestId)
        {
            var dates = _dateRepo.GetByLeaveId(leaveRequestId);
            foreach (var d in dates)
                _dateRepo.Delete(d.Id);

            return _leaveRepo.Delete(leaveRequestId);
        }

        // Helper: map Leave model to DTO
        private static LeaveReadDTO MapToReadDTO(Leave leave)
        {
            return new LeaveReadDTO
            {
                LeaveRequest_Id = leave.LeaveRequest_Id,
                Employee_Id = leave.Employee_Id,
                Leave_Type = leave.Leave_Type,
                Description = leave.Description,
                Status = leave.Status,
                Comment = leave.Comment,
                Dates = new List<DateReadDTO>() // populated later
            };
        }
    }
}
