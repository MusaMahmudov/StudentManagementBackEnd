using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.Exceptions.ExamExceptions;
using StudentManagement.Business.Exceptions.ExamTypeExceptions;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
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
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IExamTypeRepository _examTypeRepository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        private readonly IMapper _mapper;
        public ExamService(IExamRepository examRepository,IMapper mapper,IGroupSubjectRepository groupSubjectRepository,IExamTypeRepository examTypeRepository) 
        {
            _examTypeRepository = examTypeRepository;
            _groupSubjectRepository = groupSubjectRepository;
            _mapper = mapper;
         _examRepository = examRepository;
        }
        public async Task<List<GetExamDTO>> GetAllExamsAsync(string? search)
        {
            var exams = await _examRepository.GetFiltered(e=> search != null ? e.Name.Contains(search) : true, "ExamType", "GroupSubject.Group", "GroupSubject.Subject", "ExamResults.Student").ToListAsync();
            var examsDTO = _mapper.Map<List<GetExamDTO>>(exams);
            return examsDTO;
        }

        public async Task<GetExamDTO> GetExamByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetSingleAsync(e=>e.Id == id, "ExamType", "GroupSubject.Group", "GroupSubject.Subject", "ExamResults.Student");
            if (exam is null)
                throw new ExamNotFoundByIdException("Exam not found");

            var examDTO = _mapper.Map<GetExamDTO>(exam);
            return examDTO;
        }
        public async Task<GetExamForUpdateDTO> GetExamByIdForUpdateAsync(Guid id)
        {
            var exam = await _examRepository.GetSingleAsync(e => e.Id == id, "ExamType", "GroupSubject.Group", "GroupSubject.Subject", "ExamResults.Student");
            if (exam is null)
                throw new ExamNotFoundByIdException("Exam not found");

            var examDTO = _mapper.Map<GetExamForUpdateDTO>(exam);
            return examDTO;
        }
        public async Task CreateExamAsync(PostExamDTO postExamDTO)
        {
            if (!await _examTypeRepository.IsExistsAsync(et => et.Id == postExamDTO.ExamTypeId))
                throw new ExamTypeNotFoundByIdException("Exam's type not found");

            if( !await _groupSubjectRepository.IsExistsAsync(gs=>gs.Id == postExamDTO.GroupSubjectId))
                throw new GroupSubjectNotFoundByIdException("group's subject not found");

            var newExam = _mapper.Map<Exam>(postExamDTO);
            
            
           await _examRepository.CreateAsync(newExam);
          await  _examRepository.SaveChangesAsync();

        }
        public async Task UpdateExamAsync(Guid id, PutExamDTO putExamDTO)
        {
            var Exam = await _examRepository.GetSingleAsync(e => e.Id == id, "ExamType", "GroupSubject.Group", "GroupSubject.Subject");
            if (Exam is null)
                throw new ExamNotFoundByIdException("Exam not found");
            if(putExamDTO.ExamTypeId is not null && Exam.ExamTypeId != putExamDTO.ExamTypeId)
            {
                if (!await _examTypeRepository.IsExistsAsync(et => et.Id == putExamDTO.ExamTypeId))
                    throw new ExamTypeNotFoundByIdException("Exam type not found");
            }
            if(putExamDTO.GroupSubjectId is not null && Exam.GroupSubjectId != putExamDTO.GroupSubjectId)
            {
                if (!await _groupSubjectRepository.IsExistsAsync(gs => gs.Id == putExamDTO.GroupSubjectId))
                    throw new GroupSubjectNotFoundByIdException("Group subject not found");
            }

            Exam = _mapper.Map(putExamDTO, Exam);
            _examRepository.Update(Exam);
           await _examRepository.SaveChangesAsync();
            


        }

        public async Task DeleteExamAsync(Guid id)
        {
            var Exam = await _examRepository.GetSingleAsync(e => e.Id == id, "ExamType", "GroupSubject.Group", "GroupSubject.Subject");
            if (Exam is null)
                throw new ExamNotFoundByIdException("Exam not found");
            _examRepository.Delete(Exam);
           await _examRepository.SaveChangesAsync();


        }

      

        
    }
}
