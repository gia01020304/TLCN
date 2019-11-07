using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastActive { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
