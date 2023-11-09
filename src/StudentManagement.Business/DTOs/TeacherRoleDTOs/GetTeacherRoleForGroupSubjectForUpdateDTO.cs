using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherRoleDTOs
{
    public class GetTeacherRoleForGroupSubjectForUpdateDTO
    {
        public Guid TeacherId { get; set; }
        public Guid TeacherRoleId { get; set; } 
    }
}
