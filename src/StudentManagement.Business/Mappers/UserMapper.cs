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
        }
    }
}
