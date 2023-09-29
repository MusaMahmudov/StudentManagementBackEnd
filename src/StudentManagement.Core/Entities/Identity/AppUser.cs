using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        
        public bool IsActive { get; set; }
        public Student? Student { get; set; }
    }
}
