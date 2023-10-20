using AutoMapper;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
         CreateMap<PostUserDTO,AppUser>().ReverseMap();
            CreateMap<PutUserDTO, AppUser>().ReverseMap();
            CreateMap<AppUser, GetUserForUpdateDTO>().ForMember(gu => gu.TeacherId, x => x.MapFrom(u => u.Teacher.Id)).ForMember(gu => gu.StudentId, x => x.MapFrom(u => u.Student.Id)).ReverseMap();
            CreateMap<AppUser, GetUserDetailsDTO>().ForMember(gu => gu.TeacherName, x => x.MapFrom(u => u.Teacher.FullName)).ForMember(gu => gu.StudentName, x => x.MapFrom(u => u.Student.FullName)).ReverseMap();

            CreateMap<AppUser, GetUserDTO>().ForMember(gu => gu.TeacherName, x => x.MapFrom(u=>u.Teacher.FullName)).ForMember(gu => gu.StudentName, x => x.MapFrom(u => u.Student.FullName)).ReverseMap();

        }
    }
}
