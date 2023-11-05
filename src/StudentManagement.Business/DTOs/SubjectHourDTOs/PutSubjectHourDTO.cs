using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.SubjectHourDTOs
{
    public class PutSubjectHourDTO
    {
        public Guid LessonTypeId { get; set; }
        public int Room { get; set; }
        public Guid GroupSubjectId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        [DataType(DataType.Date)]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
