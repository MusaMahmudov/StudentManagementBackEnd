using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.FacultyDTOs;
using StudentManagement.Business.Exceptions.Faculty;
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
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;
        public FacultyService(IFacultyRepository facultyRepository,IMapper mapper)
        {
            _mapper = mapper;
            _facultyRepository = facultyRepository;
        }
        public async Task<List<GetFacultyDTO>> GetAllFacultiesAsync(string? search)
        {
            var Faculties = await _facultyRepository.GetFiltered(f=> search != null ? f.Name.Contains(search) : true).ToListAsync();
            return _mapper.Map<List<GetFacultyDTO>>(Faculties);
        }

        public async Task<GetFacultyDTO> GetFacultyByIdAsync(Guid id)
        {
            var Faculty = await _facultyRepository.GetSingleAsync(f=>f.Id == id);
            if(Faculty is null)
            {
                throw new FacultyNotFoundByIdException("Faculty not found");

            }
            return _mapper.Map<GetFacultyDTO>(Faculty);
        }
        public async Task<GetFacultyForUpdateDTO> GetFacultyByIdForUpdateAsync(Guid id)
        {
            var Faculty = await _facultyRepository.GetSingleAsync(f => f.Id == id);
            if (Faculty is null)
            {
                throw new FacultyNotFoundByIdException("Faculty not found");

            }
            return _mapper.Map<GetFacultyForUpdateDTO>(Faculty);
        }
        public async Task CreateFacultyAsync(PostFacultyDTO postFacultyDTO)
        {
            var newFaculty = _mapper.Map<Faculty>(postFacultyDTO);
            await _facultyRepository.CreateAsync(newFaculty);
           await _facultyRepository.SaveChangesAsync();
        }

        public async Task DeleteFacultyAsync(Guid id)
        {
            var Faculty = await _facultyRepository.GetSingleAsync(f => f.Id == id);
            if(Faculty is null)
            {
                throw new FacultyNotFoundByIdException("Faculty not found");
            }
            _facultyRepository.Delete(Faculty);
            await _facultyRepository.SaveChangesAsync();
        }

        

        public async Task UpdateFacultyAsync(Guid id, PutFacultyDTO putFacultyDTO)
        {
            var Faculty = await _facultyRepository.GetSingleAsync(f => f.Id == id);
            if (Faculty is null)
            {
                throw new FacultyNotFoundByIdException("Faculty not found");
            }
            Faculty = _mapper.Map(putFacultyDTO, Faculty);
            _facultyRepository.Update(Faculty);
            await _facultyRepository.SaveChangesAsync();
        }

       
    }
}
