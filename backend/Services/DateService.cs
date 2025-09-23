using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class DateService(DateRepository repository)
    {
        private readonly DateRepository _repository = repository;

        public DateReadDTO AddDate(DateCreateDTO dto)
        {
            var date = new Dates
            {
                Leave_Id = dto.Leave_Id,
                Hours = dto.Hours,
                Date = dto.Date
            };

            return _repository.Add(date);
        }

        public List<DateReadDTO> GetDatesByLeaveId(int leaveId)
        {
            return _repository.GetByLeaveId(leaveId);
        }

        public bool DeleteDate(int id)
        {
            return _repository.Delete(id);
        }
    }
}
