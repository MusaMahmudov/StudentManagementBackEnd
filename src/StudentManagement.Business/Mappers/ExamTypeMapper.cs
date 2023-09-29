using AutoMapper;
using StudentManagement.Business.DTOs.ExamTypeDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class ExamTypeMapper : Profile
    {
        public ExamTypeMapper() 
        {
            CreateMap<ExamType, GetExamTypeDTO>().ReverseMap();
            CreateMap<PostExamTypeDTO,ExamType>().ReverseMap();
        }
    }
}
