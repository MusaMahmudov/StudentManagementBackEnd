using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.Exceptions.TeacherExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public TeacherService(ITeacherRepository teacherRepository,IMapper mapper,UserManager<AppUser> userManager,AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }
        public async Task<List<GetTeacherDTO>> GetAllTeachersAsync(string? search)
        {
            var Teachers =await _teacherRepository.GetFiltered(t=> search != null ? t.FullName.Contains(search) : true,"AppUser", "teacherSubjects.GroupSubject.Group.Faculty", "teacherSubjects.GroupSubject.Subject", "teacherSubjects.TeacherRole").ToListAsync();
            var getTeachersDTO = _mapper.Map<List<GetTeacherDTO>>(Teachers);
            return getTeachersDTO;
        }

        public async Task<GetTeacherDTO> GetTeacherByIdAsync(Guid id)
        {
         var teacher =  await _teacherRepository.GetSingleAsync(t=>t.Id == id, "AppUser", "teacherSubjects.GroupSubject.Group.Faculty", "teacherSubjects.GroupSubject.Subject", "teacherSubjects.TeacherRole");
            if (teacher is null)
                throw new TeacherNotFoundByIdException("Teacher not found");

            var getTeacherDTO =_mapper.Map<GetTeacherDTO>(teacher);
            return getTeacherDTO;
        }
        public async Task<GetTeacherForUpdateDTO> GetTeacherByIdForUpdate(Guid id)
        {
            var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == id, "AppUser", "teacherSubjects.GroupSubject.Group.Faculty", "teacherSubjects.GroupSubject.Subject", "teacherSubjects.TeacherRole");
            if (teacher is null)
                throw new TeacherNotFoundByIdException("Teacher not found");

            var getTeacherDTO = _mapper.Map<GetTeacherForUpdateDTO>(teacher);
            return getTeacherDTO;
        }

        public async Task CreateTeacherAsync(PostTeacherDTO postTeacherDTO)
        {
            if(postTeacherDTO.AppUserId is not null)
            {
              var user = await _context.Users.Include(u=>u.Teacher).Include(u=>u.Student).FirstOrDefaultAsync(u=>u.Id == postTeacherDTO.AppUserId);
                if(user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if(user.Teacher is  not null)
                {
                    throw new UserAlreadyHasTeacherException("User is already taken");
                }
                if(user.Student is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the Student ");
                }

            }
            if(postTeacherDTO.FullName.Trim().Length < 3)
            {
                throw new TeacherFullNameMinimumLength("Full Name must minimum 3 length");
            }


            var newTeacher = _mapper.Map<Teacher>(postTeacherDTO);
            await _teacherRepository.CreateAsync(newTeacher);
            await _teacherRepository.SaveChangesAsync();

        }

        public async Task DeleteTeacherAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == id);
            if(teacher is null)
            {
                throw new TeacherNotFoundByIdException("Teacher not Found");
            }
            _teacherRepository.Delete(teacher);
          await  _teacherRepository.SaveChangesAsync();
        }

       
        public async Task UpdateTeacherAsync(Guid id, PutTeacherDTO putTeacherDTO)
        {
            var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == id,"AppUser");
            if (teacher is null)
            {
                throw new TeacherNotFoundByIdException("Teacher not Found");
            }
            if (putTeacherDTO.FullName.Trim().Length < 3)
            {
                throw new TeacherFullNameMinimumLength("Full Name must minimum 3 length");
            }

            if (putTeacherDTO.AppUserId is not null)
            {
                var user = await _context.Users.Include(u => u.Teacher).Include(u=>u.Student).FirstOrDefaultAsync(u => u.Id == putTeacherDTO.AppUserId);
                
                if (user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if (user.Teacher is not null && user.Teacher.Id != teacher.Id)
                {
                    throw new UserAlreadyHasTeacherException("User is already taken");
                }
                if (user.Student is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the Student ");
                    
                }

            }


            teacher = _mapper.Map(putTeacherDTO, teacher);
            _teacherRepository.Update(teacher);
            await _teacherRepository.SaveChangesAsync();
        }
    }
}
