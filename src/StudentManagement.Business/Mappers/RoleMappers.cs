using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Business.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class RoleMappers : Profile
    {
        public RoleMappers() 
        {
         CreateMap<IdentityRole,GetRoleDTO>().ReverseMap();
        }
    }
}
