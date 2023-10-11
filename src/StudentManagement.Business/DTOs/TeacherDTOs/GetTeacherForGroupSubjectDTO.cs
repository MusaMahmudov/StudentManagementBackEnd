using StudentManagement.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class GetTeacherForGroupSubjectDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
    
    }
}
