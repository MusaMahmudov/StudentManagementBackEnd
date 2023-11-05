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
            //CreateMap<Teacher, GetTeacherForGroupSubjectDTO>().ForMember(gt=>gt.RoleName,x=>x.MapFrom(t=>t.)).ReverseMap();
            CreateMap<Teacher, TeacherForTokenDTO>().ReverseMap();
            CreateMap<Teacher,GetTeacherForCreateGroupSubjectDTO>().ReverseMap();
            CreateMap<Teacher,GetTeacherDTO>().ReverseMap();
            CreateMap<Teacher, GetTeacherForUpdateDTO>().ForMember(gt=>gt.AppUserId,x=>x.MapFrom(t=>t.AppUserId)).ReverseMap();
            CreateMap<Teacher,GetTeacherForUser>().ForMember(gt=>gt.teacherName,x=>x.MapFrom(t=>t.FullName)).ReverseMap();
            CreateMap<PostTeacherDTO,Teacher>().ReverseMap();
            CreateMap<PutTeacherDTO, Teacher>().ReverseMap();
        }

    }
}
