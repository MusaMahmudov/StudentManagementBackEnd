using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherSubjectDTOs
{
    public class GetTeacherSubjectForTeacherDTO
    {
       
        public GetGroupSubjectForTeacherDTO GroupSubject { get; set; }
        public string TeacherRoleName { get; set; }
    }
}
