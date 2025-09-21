using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private readonly DesignationService _designationService;

        public DesignationController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Failed to connect to database!");
            var repository = new DesignationRepository(connectionString);
            _designationService = new DesignationService(repository);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var designations = _designationService.GetAllDesignations();
            return Ok(designations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var designation = _designationService.GetDesignationById(id);
            if (designation == null)
                return NotFound(new { message = "Designation not found" });

            return Ok(designation);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DesignationCreateDTO dto)
        {
            bool created = _designationService.CreateDesignation(dto);
            if (!created) return BadRequest(new { message = "Failed to create designation" });

            return Ok(new { message = "Designation created successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _designationService.DeleteDesignation(id);
            if (!deleted) return NotFound(new { message = "Designation not found" });

            return Ok(new { message = "Designation deleted successfully" });
        }
    }
}
