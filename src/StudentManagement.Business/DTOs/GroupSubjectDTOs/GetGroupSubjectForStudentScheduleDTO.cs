using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForStudentScheduleDTO
    {
        public string groupName { get; set; }
        public string subjectName { get; set; }
        public List<GetTeacherSubjectForSubjectHourForStudentScheduleDTO> teacherSubjects { get; set; }
    }
}
