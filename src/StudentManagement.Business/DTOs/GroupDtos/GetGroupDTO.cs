using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class GetGroupDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public byte StudentCount { get; set; }
        public byte Year { get; set; }
        public string FacultyName { get; set; }
        public List<GetStudentGroupDTO>? MainStudents { get; set; }
        public List<GetStudentGroupDTO>? SubStudents { get; set; }
        public List<GetGroupSubjectForGroupDTO>? GroupSubjects { get; set; }

    }
}
