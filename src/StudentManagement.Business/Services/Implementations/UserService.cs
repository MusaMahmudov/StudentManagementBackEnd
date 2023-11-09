using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Exceptions.AuthExceptions;
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
using Microsoft.AspNetCore.Hosting;
using StudentManagement.Business.HelperSevices.Interfaces;
using System.Security.Policy;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

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
        private readonly IWebHostEnvironment _webHost;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;



        public UserService(LinkGenerator linkGenerator,IHttpContextAccessor httpContextAccessor,IMailService mailService,IWebHostEnvironment webHost,ITeacherRepository teacherRepository,RoleManager<IdentityRole> roleManager,IStudentRepository studentRepository,AppDbContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _linkGenerator = linkGenerator;

            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
            _webHost = webHost;
            _teacherRepository = teacherRepository;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
            _userManager= userManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task<AppUser> CreateAccountAsync(PostUserDTO postUserDTO)
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
            newUser.Student = student;

            

            var result = await _userManager.CreateAsync(newUser, postUserDTO.Password);
            
            if (!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }
            newUser.IsActive = false;




            foreach (var roleId in postUserDTO.RoleId)
            {
                //var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

                if (role is not null)
                {
                    var resultRole = await _userManager.AddToRoleAsync(newUser, role.Name);


                }
            }


            //var link = await GetEmailConfirmationLinkAsync(newUser);
            //var mailRequestDTO = new MailRequestDTO()
            //{
            //    Subject = "Confirm Email",
            //    ToEmail = postUserDTO.Email,
            //    Body = $"<h1>Please confirm your email {link} <h1>"
            //};
            //await _mailService.SendEmail(mailRequestDTO);

            

        
            return  newUser;

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
        public async Task ConfirmEmailAsync(string token, string email)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                throw new UserNotFoundByEmailException("Something went wrong"); 
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                throw new UserNotFoundByEmailException("Something went wrong");

            }
         var result =   await _userManager.ConfirmEmailAsync(user,token);
            if (!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }
            user.IsActive = true;
          await  _userManager.UpdateAsync(user);
            

        }
        //private async Task<string?> GetEmailConfirmationLinkAsync(AppUser user)
        //{
        //    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var httpContext = _httpContextAccessor.HttpContext;
        //    var request = httpContext.Request;

        //    string? url = _linkGenerator.GetUriByAction(
        //        httpContext,
        //        action: "ConfirmEmail",
        //        controller: "Accounts",
        //        values: new { token, email = user.Email },
        //        scheme: request.Scheme,
        //        host: request.Host
        //    );

        //    return url;
        //}
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
            if(putUserDTO.Password is not null && putUserDTO.ConfirmPassword is not null) 
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
             var resetResult =  await _userManager.ResetPasswordAsync(user, token, putUserDTO.Password);
                if(!resetResult.Succeeded) 
                {
                    throw new UserUpdateFailException(resetResult.Errors);
                }

            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new UserUpdateFailException(result.Errors);
            }
            
           await _context.SaveChangesAsync();


        }
        public async Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO, string link)
        {
            if (forgotPasswordDTO.Email is null)
            {
                throw new EmailRequiredException("Email is required");
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user is null)
            {
                throw new UserNotFoundByEmailException("User not found by email");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string path = Path.Combine(_webHost.WebRootPath, "assets", "Templates", "reset-password.html");
            StreamReader str = new StreamReader(path);
            var result = await str.ReadToEndAsync();
           var body = result.Replace("[link]",link);

            MailRequestDTO mailRequest = new MailRequestDTO()
            {
                ToEmail = forgotPasswordDTO.Email,
                Subject = "Reset password",
                Body = body,
            };
            await _mailService.SendEmail(mailRequest);


        }

        public async Task<List<GetUsersForStudentAndTeacherUpdateDTO>> GetUsersForTeacherAndStudentUpdateAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDTO = _mapper.Map<List<GetUsersForStudentAndTeacherUpdateDTO>>(users);
            return usersDTO;
        }
    }
}
