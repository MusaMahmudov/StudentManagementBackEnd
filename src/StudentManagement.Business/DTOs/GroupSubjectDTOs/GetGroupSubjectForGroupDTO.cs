using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForGroupDTO
    {
        public Guid Id { get; set; }
        public GetSubjectDTO Subject { get; set; }
        public string groupName { get; set; }
        public List<GetExamForStudentPageDTO> Exams { get; set; }
        public List<GetTeacherSubjectForGroupDTO>? TeacherRoles { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public List<GetSubjectHourForStudentScheduleDTO>? subjectHours { get; set; }

    }
}
