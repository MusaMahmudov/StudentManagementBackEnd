using AutoMapper;
using StudentManagement.Business.DTOs.LessonTypeDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class LessonTypeMappers : Profile
    {
        public LessonTypeMappers() 
        {
            CreateMap<LessonType,GetLessonTypeForUpdateDTO>().ReverseMap();
            CreateMap<LessonType,GetLessonTypeDTO>().ReverseMap();
            CreateMap<PostLessonTypeDTO,LessonType>().ReverseMap();
            CreateMap<PutLessonTypeDTO, LessonType>().ReverseMap();
            CreateMap<LessonType,GetLessonTypeForSubjectHourForStudentPageDTO>().ReverseMap();
        }
    }
}
