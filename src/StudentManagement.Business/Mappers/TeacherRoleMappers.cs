using AutoMapper;
using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class TeacherRoleMappers : Profile
    {
        public TeacherRoleMappers() 
        {
            CreateMap<TeacherRole,GetTeacherRoleDTO>().ReverseMap();
            CreateMap<PostTeacherRoleDTO,TeacherRole>().ReverseMap();
            CreateMap<PutTeacherRoleDTO, TeacherRole>().ReverseMap();
        
        }
    }
}
