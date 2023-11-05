using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public string groupName { get; set; }
        public GetSubjectDTO Subject { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public List<TeacherSubjectForTeacherPageDTO>? teacherSubjects { get; set; }
        public List<GetExamForTeacherPageDTO>? Exams { get; set; }
    }
}
