<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28

namespace backend.DTOs
{
    public class TeamCreateDTO
    {
<<<<<<< HEAD
        public string Team_Name { get; set; } = string.Empty;
=======
        public string Team_Name { get; set; }
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
        public int? Manager_Id { get; set; }
    }

    public class TeamReadDTO
    {
        public int Team_Id { get; set; }
<<<<<<< HEAD
        public string Team_Name { get; set; } = string.Empty;
=======
        public string Team_Name { get; set; }
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
        public int? Manager_Id { get; set; }
    }

    public class TeamUpdateDTO
    {
        public int Team_Id { get; set; }
        public string? Team_Name { get; set; } //for updating the Team Name
        public int? Manager_Id { get; set; } //for updating the Team Manager
    }

    public class TeamDeleteDTO
    {
        public int Team_Id { get; set; }
    }
}