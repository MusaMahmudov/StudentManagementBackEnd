﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserService(AppDbContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _userManager= userManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateAccountAsync(PostUserDTO postUserDTO)
        {
           

            var newUser = _mapper.Map<AppUser>(postUserDTO);
            newUser.IsActive = true;

            var result =  await _userManager.CreateAsync(newUser,postUserDTO.Password);
            if(!result.Succeeded) 
            {
                throw new CreateUserFailException(result.Errors);
            }
        }
    }
}