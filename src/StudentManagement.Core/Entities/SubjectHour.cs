using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class SubjectHour : BaseSectionEntity
    {
        public GroupSubject GroupSubject { get; set; }
        public Guid GroupSubjectId { get; set; }

        public LessonType LessonType { get; set; }
        public Guid LessonTypeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public int Room { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

    }
}
