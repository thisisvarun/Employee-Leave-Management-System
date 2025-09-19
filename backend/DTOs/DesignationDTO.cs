<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28

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