using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Dates")]
    public class Dates
    {
        public int Id { get; set; }
        public int Leave_Id { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
    }
}
