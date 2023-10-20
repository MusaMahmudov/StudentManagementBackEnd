using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class PutGroupSubjectDTO
    {
        public Guid GroupId { get; set; }
        public Guid SubjectId { get; set; }
        public List<PostTeacherSubjectRoleDTO>? teacherRole { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
    }
}
