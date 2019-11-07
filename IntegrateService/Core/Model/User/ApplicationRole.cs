using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public ApplicationRole(string nameRole)
        {
            this.Name = nameRole;
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
