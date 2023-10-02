using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class Group : BaseSectionEntity
    {
        public string Name { get;set; }
        public byte StudentCount { get; set; }
        public byte Year { get; set; }
        public Faculty Faculty { get; set; }
        public Guid FacultyId { get; set; }
        public List<StudentGroup>? studentGroups { get; set; }
        public List<GroupSubject>? GroupSubjects { get; set; }
    }
}
