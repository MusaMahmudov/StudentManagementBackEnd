using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForTeacherScheduleDTO
    {
        public string groupName { get; set; }
        public string subjectName { get; set; }
        public List<GetTeacherSubjectForSubjectHourForTeacherScheduleDTO> teacherSubjects { get; set; }
    }
}
