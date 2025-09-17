using System;

namespace backend.DTOs
{
    public class DesignationCreateDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    public class DesignationReadDTO
    {
        public int Designation_Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DesignationDeleteDTO
    {
        public int Designation_Id { get; set; }
    }
}