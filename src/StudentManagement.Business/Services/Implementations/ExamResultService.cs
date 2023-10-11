using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Business.Exceptions.ExamExceptions;
using StudentManagement.Business.Exceptions.ExamResultExceptions;
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
    public class ExamResultService : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IMapper _mapper;
        private readonly IExamRepository _examRepository;
        private readonly IStudentRepository _studentRepository;
        public ExamResultService(IExamResultRepository examResultRepository,IMapper mapper,IExamRepository examRepository,IStudentRepository studentRepository)
        {
            _examRepository = examRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _examResultRepository = examResultRepository;
        }
        public async Task<List<GetExamResultDTO>> GetAllExamResultsAsync(string? studentName)
        {
         var examResults =  await _examResultRepository.GetFiltered(er=> studentName != null ? er.Student.FullName.Contains(studentName) : true,"Student", "Exam.ExamType", "Exam.GroupSubject.Group", "Exam.GroupSubject.Subject").ToListAsync();
            var examResultsDTO = _mapper.Map<List<GetExamResultDTO>>(examResults);
            return examResultsDTO;
        }

        public async Task<GetExamResultDTO> GetExamResultByIdAsync(Guid id)
        {
            var examResult = await _examResultRepository.GetSingleAsync(er => er.Id == id,"Student","Exam.ExamType", "Exam.GroupSubject.Group", "Exam.GroupSubject.Subject");
            if (examResult is null)
                throw new ExamResultNotFoundByIdException("Exam's result not found");
            var examResultDTO = _mapper.Map<GetExamResultDTO>(examResult);
            return examResultDTO;



        }

        public async Task CreateExamResultAsync(PostExamResultDTO postExamResultDTO)
        {
            if (!await _examRepository.IsExistsAsync(e => e.Id == postExamResultDTO.ExamId))
                throw new ExamNotFoundByIdException("Exam not found");
            if (postExamResultDTO.Score > (await _examRepository.GetSingleAsync(e => e.Id == postExamResultDTO.ExamId)).MaxScore)
                throw new ExamResultScoreCannotBeMoreThanMaxScoreException("Result more than max score");

            if (!await _studentRepository.IsExistsAsync(s => s.Id == postExamResultDTO.StudentId))
                throw new StudentNotFoundByIdException("Student bot found");
            




            var newExamResult = _mapper.Map<ExamResult>(postExamResultDTO);
           await _examResultRepository.CreateAsync(newExamResult);
           await _examResultRepository.SaveChangesAsync();


        }
        public async Task UpdateExamResultAsync(Guid id, PutExamResultDTO putExamResultDTO)
        {
            var examResult = await _examResultRepository.GetSingleAsync(e => e.Id == id);
            if (examResult is null)
                throw new ExamResultNotFoundByIdException("Exam's result not found");


            if (!await _examRepository.IsExistsAsync(e => e.Id == putExamResultDTO.ExamId))
                throw new ExamNotFoundByIdException("Exam not found");
            if (putExamResultDTO.Score > (await _examRepository.GetSingleAsync(e => e.Id == putExamResultDTO.ExamId)).MaxScore)
                throw new ExamResultScoreCannotBeMoreThanMaxScoreException("Result more than max score");

            if (!await _studentRepository.IsExistsAsync(s => s.Id == putExamResultDTO.StudentId))
                throw new StudentNotFoundByIdException("Student bot found");

            

            examResult = _mapper.Map(putExamResultDTO,examResult);
            _examResultRepository.Update(examResult);
           await _examResultRepository.SaveChangesAsync();

                

        }

        public async Task DeleteExamResultAsync(Guid id)
        {
            var examResult = await _examResultRepository.GetSingleAsync(e => e.Id == id);
            if (examResult is null)
                throw new ExamResultNotFoundByIdException("Exam's result not found");

            _examResultRepository.Delete(examResult);
           await _examResultRepository.SaveChangesAsync();

        }

      
       
    }
}
