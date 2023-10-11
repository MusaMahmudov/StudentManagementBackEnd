using AutoMapper;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class TeacherSubjectMapper : Profile
    {
        public TeacherSubjectMapper() 
        {
            CreateMap<PostTeacherSubjectRoleDTO, TeacherSubject>().ReverseMap();
            CreateMap<TeacherSubject, GetTeacherSubjectForTeacherDTO>().ForMember(gt => gt.TeacherRoleName, x => x.MapFrom(ts => ts.TeacherRole.Name)).ReverseMap();

        }
    }
}
