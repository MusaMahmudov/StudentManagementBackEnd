using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Exceptions.RoleExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.TeacherExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Enums;
using StudentManagement.DataAccess.Repositories.Interfaces;
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
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        
        
        public UserService(ITeacherRepository teacherRepository,RoleManager<IdentityRole> roleManager,IStudentRepository studentRepository,AppDbContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _teacherRepository = teacherRepository;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
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
                student = await _studentRepository.GetSingleAsync(s => s.Id == (Guid)postUserDTO.StudentId);
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
                //var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

                if (role is not null)
                {
                    var resultRole = await _userManager.AddToRoleAsync(newUser, role.Name);


                }
            }

        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.Users.Include(u=>u.Student).Include(u=>u.Teacher).FirstOrDefaultAsync(u=>u.Id == id);
            if(user is null)    
                throw new UserNotFoundByIdException("User Not found");
            if(user.Student is not null)
            {
                var student = await _studentRepository.GetSingleAsync(s => s.Id == user.Student.Id);
                if(student is null)
                {
                    throw new StudentNotFoundByIdException("Student not found");
                }
                student.AppUser = null;
                _studentRepository.Update(student);
            }
            if (user.Teacher is not null)
            {
                var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == user.Teacher.Id);
                if (teacher is null)
                {
                    throw new TeacherNotFoundByIdException("Teacher not found");
                }
                teacher.AppUser = null;
                _teacherRepository.Update(teacher);
            }

            var identityResult = await _userManager.DeleteAsync(user);
            

        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync()
        {
            var Users = await _userManager.Users.Include(u=>u.Student).Include(u=>u.Teacher).ToListAsync();
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

        public async Task<GetUserDetailsDTO> GetUserByIdAsync(string Id)
        {
            var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u=>u.Id == Id);
            if (user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            var userDTO = _mapper.Map<GetUserDetailsDTO>(user);
           
                var roles = await _userManager.GetRolesAsync(user);

                
                userDTO.Roles = roles.ToList();
         


            


            return userDTO;
        }

        public async Task<GetUserForUpdateDTO> GetUserByIdForUpdateAsync(string Id)
        {
            var user = await _userManager.Users.Include(u => u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Id == Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            var userDTO = _mapper.Map<GetUserForUpdateDTO>(user);
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            List<string> RolesId = new List<string>();
            var roles = await _userManager.GetRolesAsync(user);
            if(roles.Count() > 0)
            {
                

                foreach (var role in roles)
                {
                     identityRoles = await _roleManager.Roles.Where(r => r.Name == role).ToListAsync();

                }
                foreach (var roleIdentity in identityRoles)
                {
                  var roleId =  await _roleManager.GetRoleIdAsync(roleIdentity);
                    RolesId.Add(roleId);
                }
            }


            userDTO.RoleId = RolesId;
            return userDTO;

        }

        public async Task UpdateUserAsync(string Id,PutUserDTO putUserDTO)
        {
            //var user = await _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u => u.Id == Id);
            var user = await _userManager.Users.Include(u=>u.Student).Include(u=>u.Teacher).FirstOrDefaultAsync(u=>u.Id == Id);
            if (user is null) 
            {
             throw new UserNotFoundByIdException("User not found");
            }
            if(putUserDTO.TeacherId is not null && putUserDTO.StudentId is not null)
            {
                throw new UserDTOTeacherAndStudentException("Student and teacher cant be assigned at the same time");
            }
            user = _mapper.Map(putUserDTO, user);


            if (putUserDTO.RoleId?.Count()> 0)
            {
                List<string>? newRoles  = new List<string>();
                foreach(var roleId in putUserDTO.RoleId)
                {
                    var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
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
                var existingStudent = await _studentRepository.GetSingleAsync(s => s.Id == putUserDTO.StudentId,"AppUser");
                if (existingStudent is null)
                {
                    throw new StudentNotFoundByIdException("Student not found");
                }
                if(existingStudent.AppUser is not null && existingStudent.AppUser.Id != user.Id)
                {
                    throw new StudentAlreadyHasAccountException("Student already has account");
                }

                user.Student = existingStudent;
                existingStudent.AppUser = user;
                _studentRepository.Update(existingStudent);

            }
            else
            {
                if(student?.AppUser is not null)
                {
                    student.AppUser = null;
                    user.Student = null;
                    _studentRepository.Update(student);

                }

            }


            Teacher? teacher = user.Teacher;
            if (putUserDTO.TeacherId is not null)
            {
                var existingTeacher = await _teacherRepository.GetSingleAsync(t => t.Id == putUserDTO.TeacherId,"AppUser");
                if (existingTeacher is null)
                {
                    throw new TeacherNotFoundByIdException("Teacher not Found");
                }
                if (existingTeacher.AppUser is not null && existingTeacher.AppUser.Id != user.Id)
                {
                    throw new StudentAlreadyHasAccountException("Teacher already has account");
                }

                existingTeacher.AppUser = user;
                user.Teacher = existingTeacher;
                _teacherRepository.Update(existingTeacher);
            }
            else
            {
                if (teacher?.AppUser is not null)
                {
                    teacher.AppUserId = null;
                    user.Teacher = null;
                    _teacherRepository.Update(teacher);

                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new UserUpdateFailException(result.Errors);
            }
            
           await _context.SaveChangesAsync();


        }
    }
}
