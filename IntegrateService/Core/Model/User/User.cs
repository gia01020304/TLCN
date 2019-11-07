using System;
using System.Collections.Generic;
using System.Text;

namespace General.Model.User
{
    public class User
    {
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
