using AutoMapper;
using StudentManagement.Business.DTOs.Attendance;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class AttendanceMapper : Profile
    {
        public AttendanceMapper() 
        {
            CreateMap<Attendance,GetAttendanceForAttendanceForTeacherPageDTO>().ForMember(ga => ga.Date, x => x.MapFrom(a => a.SubjectHour.Date)).ReverseMap();
         CreateMap<Attendance,GetAttendanceForStudentAttendancePageDTO>().ForMember(ga=>ga.Date,x=>x.MapFrom(a=>a.SubjectHour.Date)).ReverseMap();
            CreateMap<Attendance,GetAttendanceForTeacherPageDTO>().ReverseMap();
            CreateMap<PutAttendanceDTO,Attendance>().ReverseMap();
        }
    }
}
