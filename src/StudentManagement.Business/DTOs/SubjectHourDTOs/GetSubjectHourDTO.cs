using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.LessonTypeDTOs;
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
        public Guid Id { get; set; }
        public GetGroupSubjectForStudentScheduleDTO GroupSubject { get; set; }

        public GetLessonTypeForSubjectHourForStudentPageDTO LessonType { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public int Room { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}
