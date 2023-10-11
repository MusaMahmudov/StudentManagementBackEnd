using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Business.Exceptions.SubjectExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository subjectRepository,IMapper mapper)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<List<GetSubjectDTO>> GetAllSubjectAsync(string? search)
        {
           var subjects = await _subjectRepository.GetFiltered(s=> search != null ? s.Name.Contains(search) : true).ToListAsync();
            var getSubjectDTO = _mapper.Map<List<GetSubjectDTO>>(subjects);
            return getSubjectDTO;
        }

        public async Task<GetSubjectDTO> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetSingleAsync(s=>s.Id == id);
            if (subject is null)
                throw new SubjectNotFoundByIdException("Subject not found");

            var getSubjectDTO = _mapper.Map<GetSubjectDTO>(subject);
            return getSubjectDTO;

        }

        public async Task CreateSubjectAsync(PostSubjectDTO postSubjectDTO)
        {
            var newSubject = _mapper.Map<Subject>(postSubjectDTO);


           await _subjectRepository.CreateAsync(newSubject);
            await _subjectRepository.SaveChangesAsync();
           

        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectRepository.GetSingleAsync(s => s.Id == id);
            if (subject is null)
                throw new SubjectNotFoundByIdException("Subject not found");

            

            _subjectRepository.Delete(subject);
           await _subjectRepository.SaveChangesAsync();

        }

        public async Task UpdateSubjectAsync(Guid id, PutSubjectDTO putSubjectDTO)
        {
            var subject =await  _subjectRepository.GetSingleAsync(s => s.Id == id);
            if (subject is null)
                throw new SubjectNotFoundByIdException("Subject not found");
            subject = _mapper.Map(putSubjectDTO,subject);
            _subjectRepository.Update(subject);
           await _subjectRepository.SaveChangesAsync();

        }
    }
}
