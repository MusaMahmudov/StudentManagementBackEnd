using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
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
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<List<GetStudentDTO>> GetAllStudentsAsync(string? search)
        {
            var students = await _studentRepository.GetFiltered(s => search != null ? s.FullName.Contains(search) : true).ToListAsync();

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

        public async Task UpdateStudentAsync(Guid Id, PostStudentDTO postStudentDTO)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            student = _mapper.Map(postStudentDTO, student);
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();


        }


    }
}
