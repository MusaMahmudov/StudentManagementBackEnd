using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherSubjectDTOs
{
    public class TeacherSubjectForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public string teacherName { get; set; }
        public string teacherRole { get; set; }
    }
}
