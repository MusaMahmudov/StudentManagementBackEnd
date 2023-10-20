using AutoMapper;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class TeacherMappers : Profile
    {
        public TeacherMappers() 
        {
            CreateMap<Teacher, GetTeacherForGroupSubjectDTO>().ReverseMap();
            CreateMap<Teacher,GetTeacherDTO>().ReverseMap();
            CreateMap<Teacher, GetTeacherForUpdateDTO>().ForMember(gt=>gt.AppUserId,x=>x.MapFrom(t=>t.AppUserId)).ReverseMap();

            CreateMap<PostTeacherDTO,Teacher>().ReverseMap();
            CreateMap<PutTeacherDTO, Teacher>().ReverseMap();
        }

    }
}
