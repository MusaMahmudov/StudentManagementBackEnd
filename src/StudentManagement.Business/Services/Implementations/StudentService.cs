using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.TeacherExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public StudentService(IStudentRepository studentRepository, IMapper mapper,AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<List<GetStudentDTO>> GetAllStudentsAsync(string? search)
        {
            var students = await _studentRepository.GetFiltered(s => search != null ? s.FullName.Contains(search) : true,"studentGroups.Group.Faculty","AppUser").ToListAsync();

            return _mapper.Map<List<GetStudentDTO>>(students);
        }

        public async Task<GetStudentDTO> GetStudentByIdAsync(Guid Id)
        {

            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            return _mapper.Map<GetStudentDTO>(student);
        }
        public async Task CreateStudentAsync(PostStudentDTO postStudentDTO)
        {
            if(postStudentDTO.AppUserId is not null)
            {
                var user = await _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u=>u.Id == postStudentDTO.AppUserId);
                if(user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if(user.Student is not null)
                {
                    throw new UserAlreadyHasStudentException("User is already taken");
                }
                if(user.Teacher is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");
                }
            }

            var student = _mapper.Map<Student>(postStudentDTO);
            await _studentRepository.CreateAsync(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Guid Id)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);
            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Guid Id, PutStudentDTO putStudentDTO)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id,"AppUser");

            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");
            if (putStudentDTO.AppUserId is not null)
            {
                var user =await  _context.Users.Include(u=>u.Student).FirstOrDefaultAsync(u=>u.Id == putStudentDTO.AppUserId);
                if(user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if(user.Student is not null )
                {
                    throw new UserAlreadyHasStudentException("User is already taken");
                }

            }



            student = _mapper.Map(putStudentDTO, student);
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();


        }
        public async Task<bool> CheckStudentExistsByIdAsync(Guid id)
        {
          return  await _studentRepository.IsExistsAsync(s=>s.Id == id);
        }


    }
}
