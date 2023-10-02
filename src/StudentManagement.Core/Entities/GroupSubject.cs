using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class GroupSubject : BaseEntity
    {
        public Group Group { get; set; }
        public Guid GroupId { get; set; }
        public Subject Subject { get; set; }
        public Guid SubjectId { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
        public List<TeacherSubject>? teacherSubjects { get; set; }
    }
}
