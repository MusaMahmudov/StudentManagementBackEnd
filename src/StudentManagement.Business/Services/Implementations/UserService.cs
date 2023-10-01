using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Exceptions.RoleExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
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

            
            if(postUserDTO.TeacherId is not null && postUserDTO.StudentId is not null) 
            {
             throw new UserDTOTeacherAndStudentException("Student and teacher cant be assigned at the same time");
            }
            Student student = null;

            if (postUserDTO.StudentId is not null)
            {
                student = await _context.Students.FirstOrDefaultAsync(s=>s.Id == (Guid)postUserDTO.StudentId);

                if (student is null) 
                {
                    throw new StudentNotFoundByIdException("Student not found");

                }

            }


            var newUser = _mapper.Map<AppUser>(postUserDTO);
            newUser.IsActive = true;
            newUser.Student = student;

            var result = await _userManager.CreateAsync(newUser, postUserDTO.Password);
            if (!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }

            foreach (var roleId in postUserDTO.RoleId)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                if (role is not null)
                {
                    var resultRole = await _userManager.AddToRoleAsync(newUser, role.Name);


                }
            }

        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync()
        {
            var Users = await _context.Users.Include(u=>u.Student).ToListAsync();
            var usersDTO = new List<GetUserDTO>();
            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userRole = new GetUserDTO()
                {
                    Roles = roles.ToList(),
                };
                userRole = _mapper.Map(user,userRole);
                usersDTO.Add(userRole);


            }


            return usersDTO;
        }
      
        public async Task UpdateUserAsync(string Id,PutUserDTO putUserDTO)
        {
            var user = await _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u => u.Id == Id);
            if (user is null) 
            {
             throw new UserNotFoundByIdException("User not found");
            }
            if(putUserDTO.TeacherId is not null && putUserDTO.StudentId is not null)
            {
                throw new UserDTOTeacherAndStudentException("Student and teacher cant be assigned at the same time");
            }
            user = _mapper.Map(putUserDTO, user);


            if (putUserDTO.RoleId is not null)
            {
                List<string>? newRoles  = new List<string>();
                foreach(var roleId in putUserDTO.RoleId)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                    if(role is null)
                    {
                        throw new RoleNotFoundByIdException($"Role with Id:{roleId} doesn't exist");
                    }
                    newRoles.Add(role.Name);
                }

                var roles = await _userManager.GetRolesAsync(user);
                
                var removeRoles = roles.Except(newRoles);
                await _userManager.RemoveFromRolesAsync(user,removeRoles);

                var rolesToAdd = newRoles.Except(roles).ToList();
                await _userManager.AddToRolesAsync(user,rolesToAdd);


            }
            Student? student = user.Student;

            if (putUserDTO.StudentId is not null)
            {
                student = await _context.Students.FirstOrDefaultAsync(s=>s.Id == putUserDTO.StudentId);
                if (student is null)
                {
                    throw new StudentNotFoundByIdException("Student not Found");
                }

            }
            user.Student = student;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new UserUpdateFailException(result.Errors);
            }
            
           await _context.SaveChangesAsync();


        }
    }
}
