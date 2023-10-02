using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class TeacherSubject : BaseEntity
    {
        public Teacher Teacher { get; set; }
        public Guid TeacherId { get; set; }
        public GroupSubject GroupSubject { get; set; }
        public Guid GroupSubjectId { get; set; }
        public TeacherRole TeacherRole { get; set; }
        public Guid TeacherRoleId { get; set; }

    }
}
