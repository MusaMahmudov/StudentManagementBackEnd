using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class GetTeacherForGroupDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string TeacherRoleName { get; set; }
    }
}
