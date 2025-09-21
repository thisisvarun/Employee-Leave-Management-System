using System;

namespace backend.Models
{
    public class Team
    {
        public int Team_Id { get; set; }
        public string Team_Name { get; set; } = string.Empty;
        public int? Manager_Id { get; set; }
    }
}