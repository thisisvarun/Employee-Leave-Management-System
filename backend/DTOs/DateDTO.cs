using System;

namespace backend.DTOs
{
    public class DateCreateDTO
    {
        public int Leave_Id { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
    }

    public class DateReadDTO
    {
        public int Id { get; set; }
        public int Leave_Id { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
    }

    public class DateDeleteDTO
    {
        public int Leave_Id { get; set; }
    }
}
