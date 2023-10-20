﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.Services.Interfaces;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService) 
        { 
        _roleService = roleService;
        }
        [HttpGet]
        public IActionResult GetAllRoles() 
        {
         var roles = _roleService.GetRoles();
            return Ok(roles);
        }
    }
}
