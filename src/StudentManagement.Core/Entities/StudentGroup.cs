using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class StudentGroup : BaseEntity
    {
        public Student Student { get; set; }
        public Guid StudentId { get; set; }
        public Group Group { get; set; }
        public Guid GroupId { get; set; }
        public bool IsSubGroup { get; set; } 

    }
}
