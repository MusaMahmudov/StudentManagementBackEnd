using AutoMapper;
using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class ExamMappers : Profile
    {
        public ExamMappers() 
        {
            CreateMap<Exam,GetExamForExamResultDTO>().ForMember(ge=>ge.ExamTypeName,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
           CreateMap<Exam,GetExamDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
            CreateMap<Exam, GetExamForUpdateDTO>().ForMember(ge => ge.ExamTypeId, x => x.MapFrom(e => e.ExamType.Id));
            CreateMap<PostExamDTO,Exam>().ReverseMap();
            CreateMap<PutExamDTO,Exam>().ReverseMap();
        }
    }
}
