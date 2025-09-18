using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Linq;
=======
using Microsoft.Data.SqlClient;
using backend.DTOs;
>>>>>>> Stashed changes

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
<<<<<<< Updated upstream
    public class LeavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeavesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Leave>> GetLeaves()
        {
            var Leaves = _context.Leaves.ToList();
            return Ok(Leaves);
        }

        [HttpGet("{id}")]
        public ActionResult<Leave> GetLeave(int id)
        {
            var Leave = _context.Leaves
                .FirstOrDefault(e => e.LeaveRequestId == id);

            if (Leave == null) return NotFound();
            return Ok(Leave);
        }

        [HttpPost]
        public ActionResult<Leave> CreateLeave(Leave leave)
        {
            _context.Leaves.Add(leave);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLeave), new { id = Leave.LeaveRequestId }, leave);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeave(int LeaveRequestId, int Employee_Id)
        {
            var Leave = _context.Leaves.Find(Leave_Id);
            if (Leave == null) return BadRequest();
            Leave.Team_Id = Team_Id;
            _context.Entry(Leave).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveDesignation(int Designation_Id, int Leave_Id)
        {
            var Leave = _context.Leaves.Find(Leave_Id);
            if (Leave == null) return BadRequest();
            Leave.Designation_Id = Designation_Id;
            _context.Entry(Leave).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
=======
    public class LeaveController : ControllerBase
    {
        private readonly string _connectionString;

        public LeaveController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new Exception("Failed to connect to database!");
            }
        }

        [HttpPost]
        public IActionResult CreateLeave(LeaveCreateDTO dto)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new(@"INSERT INTO LeaveRequest (Employee_Id, Leave_Type, Description, Comment, Status) 
                                       VALUES (@Employee_Id, @Leave_Type, @Description, @Comment, 'Pending')", conn);

                cmd.Parameters.AddWithValue("@Employee_Id", dto.Employee_Id);
                cmd.Parameters.AddWithValue("@Leave_Type", dto.Leave_Type);
                cmd.Parameters.AddWithValue("@Description", dto.Description);
                cmd.Parameters.AddWithValue("@Comment", (object?)dto.Comment ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
            return Ok("Leave request created");
        }

        [HttpGet("{id}")]
        public ActionResult<LeaveReadDTO?> GetLeaveById(int id)
        {
            LeaveReadDTO? leave = null;

            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("SELECT * FROM LeaveRequest WHERE LeaveRequest_Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    leave = new LeaveReadDTO
                    {
                        LeaveRequest_Id = (int)reader["LeaveRequest_Id"],
                        Employee_Id = (int)reader["Employee_Id"],
                        Leave_Type = reader["Leave_Type"].ToString() ?? "",
                        Description = reader["Description"].ToString() ?? "",
                        Status = reader["Status"].ToString() ?? "",
                        Comment = reader["Comment"] as string
                    };
                }
            }

            if (leave == null) return NotFound("Leave request not found");
            return Ok(leave);
        }

        [HttpPut]
        public IActionResult UpdateLeave(LeaveUpdateDTO dto)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new(@"UPDATE LeaveRequest 
                                       SET Description=@Description, Status=@Status, Comment=@Comment 
                                       WHERE LeaveRequest_Id=@LeaveRequest_Id", conn);

                cmd.Parameters.AddWithValue("@LeaveRequest_Id", dto.LeaveRequest_Id);
                cmd.Parameters.AddWithValue("@Description", (object?)dto.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object?)dto.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Comment", (object?)dto.Comment ?? DBNull.Value);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) return NotFound("Leave request not found");
            }
            return Ok("Leave request updated");
>>>>>>> Stashed changes
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeave(int id)
        {
<<<<<<< Updated upstream
            var Leave = _context.Leaves.Find(id);
            if (Leave == null) return NotFound();

            _context.Leaves.Remove(Leave);
            _context.SaveChanges();

            return NoContent();
=======
            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new("DELETE FROM LeaveRequest WHERE LeaveRequest_Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) return NotFound("Leave request not found");
            }
            return Ok("Leave request deleted");
>>>>>>> Stashed changes
        }
    }
}
