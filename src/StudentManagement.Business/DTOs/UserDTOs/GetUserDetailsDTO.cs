using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.UserDTOs
{
    public class GetUserDetailsDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string? StudentName { get; set; }
        public string? TeacherName { get; set; }
        public List<string> Roles { get; set; }
    }
}
