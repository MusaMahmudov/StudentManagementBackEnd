﻿using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> CreateAccountAsync(PostUserDTO postUserDTO);
        Task<List<GetUserDTO>> GetAllUsersAsync();
        Task<GetUserDetailsDTO> GetUserByIdAsync(string Id);
        Task<GetUserForUpdateDTO> GetUserByIdForUpdateAsync(string Id);
        Task<List<GetUsersForStudentAndTeacherUpdateDTO>> GetUsersForTeacherAndStudentUpdateAsync();
        Task ConfirmEmailAsync(string token,string email);
        Task UpdateUserAsync(string id, PutUserDTO putUserDTO);
        Task DeleteUserAsync(string id);

    }
}
