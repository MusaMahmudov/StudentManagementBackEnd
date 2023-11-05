using StudentManagement.Business.DTOs.Attendance;
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
    public class GetSubjectHourForStudentAttendancePageDTO
    {
        //public List<GetAttendanceForStudentAttendancePageDTO>? Attendances { get; set; }
        public string LessonTypeName { get; set; }
        public Guid GroupSubjectId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
