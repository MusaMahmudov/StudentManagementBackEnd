using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using StudentManagement.Business.Exceptions.TeacherRoleExceptions;
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
    public class TeacherRoleService : ITeacherRoleService
    {
        private readonly ITeacherRoleRepository _teacherRoleRepository;
        private readonly IMapper _mapper;
        public TeacherRoleService(ITeacherRoleRepository teacherRoleRepository,IMapper mapper)
        {
            _mapper = mapper;
            _teacherRoleRepository = teacherRoleRepository;
        }
        public async Task<List<GetTeacherRoleDTO>> GetAllTeacherRolesAsync(string? search)
        {
            var teacherRoles = await _teacherRoleRepository.GetFiltered(tr => search != null ? tr.Name.Contains(search) : true).ToListAsync();
            var teacherRolesDTO = _mapper.Map<List<GetTeacherRoleDTO>>(teacherRoles);
            return teacherRolesDTO;
            
        }

        public async Task<GetTeacherRoleDTO> GetTeacherRoleByIdAsync(Guid id)
        {
            var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr=>tr.Id == id);
            if (teacherRole is null)
                throw new TeacherRoleNotFoundByIdException("TeacherRole not found");
            var teacherRoleDTO = _mapper.Map<GetTeacherRoleDTO>(teacherRole);
            return teacherRoleDTO;

        }

        public async Task CreateTeacherRoleAsync(PostTeacherRoleDTO postTeacherRoleDTO)
        {
            var newTeacherRole = _mapper.Map<TeacherRole>(postTeacherRoleDTO);
           await _teacherRoleRepository.CreateAsync(newTeacherRole);
           await _teacherRoleRepository.SaveChangesAsync();


        }

        public async Task DeleteTeacherRoleAsync(Guid id)
        {
            var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr => tr.Id == id);
            if (teacherRole is null)
                throw new TeacherRoleNotFoundByIdException("TeacherRole not found");
            _teacherRoleRepository.Delete(teacherRole);
            await _teacherRoleRepository.SaveChangesAsync();

        }

      
        public async Task UpdateTeacherRoleAsync(Guid id, PutTeacherRoleDTO putTeacherRoleDTO)
        {
            var teacherRole = await _teacherRoleRepository.GetSingleAsync(tr => tr.Id == id);
            if (teacherRole is null)
                throw new TeacherRoleNotFoundByIdException("TeacherRole not found");
            teacherRole = _mapper.Map(putTeacherRoleDTO, teacherRole);
            _teacherRoleRepository.Update(teacherRole);
            await _teacherRoleRepository.SaveChangesAsync();       
        }
    }
}
