using backend.DTOs;
using backend.Models;
using backend.Repositories;
using System.Collections.Generic;

namespace backend.Services
{
    public class DesignationService(DesignationRepository repository)
    {
        private readonly DesignationRepository _repository = repository;

        public IEnumerable<DesignationReadDTO> GetAllDesignations()
        {
            var designations = _repository.GetAll();
            var result = new List<DesignationReadDTO>();

            foreach (var des in designations)
            {
                result.Add(new DesignationReadDTO
                {
                    Designation_Id = des.Designation_Id,
                    Name = des.Name
                });
            }

            return result;
        }

        public DesignationReadDTO? GetDesignationById(int id)
        {
            var des = _repository.GetById(id);
            if (des == null) return null;

            return new DesignationReadDTO
            {
                Designation_Id = des.Designation_Id,
                Name = des.Name
            };
        }

        public bool CreateDesignation(DesignationCreateDTO dto)
        {
            var des = new Designation
            {
                Name = dto.Name
            };

            return _repository.Add(des); // returns true if insert succeeded
        }

        public bool DeleteDesignation(int id)
        {
            return _repository.Delete(id); // returns true if delete succeeded
        }
    }
}
