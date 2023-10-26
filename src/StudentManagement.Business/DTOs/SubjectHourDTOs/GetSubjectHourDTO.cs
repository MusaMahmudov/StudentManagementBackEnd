using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.SubjectHourDTOs
{
    public class GetSubjectHourDTO
    {
        public GroupSubject GroupSubject { get; set; }

        public LessonType LessonType { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public int Room { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}
