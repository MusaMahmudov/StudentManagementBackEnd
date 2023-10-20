using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Business.DTOs.RoleDTOs;
using StudentManagement.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public List<GetRoleDTO> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesDTO = _mapper.Map<List<GetRoleDTO>>(roles);

            return rolesDTO;

        }
    }
}
