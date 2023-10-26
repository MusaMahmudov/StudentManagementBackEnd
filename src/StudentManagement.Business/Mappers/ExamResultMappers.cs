using AutoMapper;
using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class ExamResultMappers : Profile
    {
        public ExamResultMappers() 
        {
          CreateMap<ExamResult,GetExamResultDTO>().ForMember(ge=>ge.studentName,x=>x.MapFrom(er=>er.Student.FullName)).ReverseMap();
            CreateMap<PostExamResultDTO,ExamResult>().ReverseMap();
            CreateMap<PutExamResultDTO, ExamResult>().ReverseMap();
            CreateMap<ExamResult,GetExamResultForExamForStudentPageDTO>().ForMember(ge=>ge.studentName,x=>x.MapFrom(er=>er.Student.FullName)).ForMember(ge=>ge.studentId,x=>x.MapFrom(ge=>ge.StudentId)).ReverseMap();


            CreateMap<ExamResult,GetExamResultForExam>()
                .ForMember(ge=>ge.studentName,x=>x.MapFrom(er=>er.Student.FullName))
                .ReverseMap();
        }
    }
}
