<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28

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
<<<<<<< HEAD
}
=======
}
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
