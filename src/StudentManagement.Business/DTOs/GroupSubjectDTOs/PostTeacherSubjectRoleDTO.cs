using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class PostTeacherSubjectRoleDTO
    {
        public Guid TeacherId { get; set; }
        public Guid RoleId { get; set; }
    }
}
