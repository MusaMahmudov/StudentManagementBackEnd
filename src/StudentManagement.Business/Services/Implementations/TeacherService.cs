using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.Exceptions.TeacherExceptions;
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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public TeacherService(ITeacherRepository teacherRepository,IMapper mapper)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }
        public async Task<List<GetTeacherDTO>> GetAllTeachersAsync(string? search)
        {
            var Teachers =await _teacherRepository.GetFiltered(t=> search != null ? t.FullName.Contains(search) : true).ToListAsync();
            var getTeachersDTO = _mapper.Map<List<GetTeacherDTO>>(Teachers);
            return getTeachersDTO;
        }

        public async Task<GetTeacherDTO> GetTeacherByIdAsync(Guid id)
        {
         var teacher =  await _teacherRepository.GetSingleAsync(t=>t.Id == id);
            var getTeacherDTO =_mapper.Map<GetTeacherDTO>(teacher);
            return getTeacherDTO;
        }

        public async Task CreateTeacherAsync(PostTeacherDTO postTeacherDTO)
        {
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

       
        public async Task UpdateTeacherAsync(Guid id, PostTeacherDTO postTeacherDTO)
        {
            var teacher = await _teacherRepository.GetSingleAsync(t => t.Id == id);
            if (teacher is null)
            {
                throw new TeacherNotFoundByIdException("Teacher not Found");
            }
            teacher = _mapper.Map(postTeacherDTO, teacher);
            _teacherRepository.Update(teacher);
            await _teacherRepository.SaveChangesAsync();
        }
    }
}
