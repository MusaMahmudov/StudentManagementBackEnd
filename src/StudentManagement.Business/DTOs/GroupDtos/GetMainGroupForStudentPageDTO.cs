using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class GetMainGroupForStudentPageDTO
    {
        public string Name { get; set; }
        public byte? StudentCount { get; set; }
        public byte Year { get; set; }
        public string facultyName { get; set; }
        public List<GetStudentForGroupForStudentPageDTO>? Students { get; set; }
        public List<StudentGroup>? studentGroups { get; set; }
        public List<GetGroupSubjectForGroupDTO>? GroupSubjects { get; set; }
    }
}
