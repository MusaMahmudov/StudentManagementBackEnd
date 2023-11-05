using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class Attendance : BaseSectionEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid SubjectHourId { get; set; }
        public SubjectHour SubjectHour { get; set; }
        public bool? IsPresent { get; set; }
    }
}
