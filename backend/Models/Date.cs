using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Dates")]
    public class Dates
    {
        [Column("Leave_Id")]
        [Required]
        public int Leave_Id { get; set; }

        [Column("Hours")]
        [Required]
        public int Hours { get; set; }

        [Column("Date")]
        [Required]
        public DateTime Date { get; set; }
    }
}
