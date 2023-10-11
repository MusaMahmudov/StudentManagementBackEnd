using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.SubjectHourDTOs
{
    public class PostSubjectHourDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public Guid LessonTypeId { get; set; }
        public int Room { get; set; }
        public Guid GroupSubjectId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}
