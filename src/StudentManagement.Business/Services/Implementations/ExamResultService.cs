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
        public async Task<GetExamResultForExamForStudentPageDTO> GetExamResultForExamForStudentPageAsync(Guid examId, Guid studentId)
        {
          var examResult =  await _examResultRepository.GetSingleAsync(er=>er.ExamId == examId && er.StudentId == studentId,"Exam.ExamType","Student");
            if(examResult is null)
            {
                throw new ExamResultNotFoundByIdException($"Exam's result not found");
            }
            var examResultDTO = _mapper.Map<GetExamResultForExamForStudentPageDTO>(examResult);
            return examResultDTO;
        }
        public async Task<List<GetExamResultForExamForStudentPageDTO>> GetExamResultsForFinalExamForStudentPageAsync(Guid studentId)
        {
           var examResults = await _examResultRepository.GetFiltered(er=>er.StudentId == studentId && er.Exam.ExamType.Name != "Final", "Exam.ExamType", "Student").ToListAsync();
            var examResultsDTO = _mapper.Map<List<GetExamResultForExamForStudentPageDTO>>(examResults);
            return examResultsDTO;

        }
        public async Task<GetExamResultForUpdateDTO> GetExamResultForUpdateAsync(Guid Id)
        {
           var examResult = await _examResultRepository.GetSingleAsync(er=>er.Id == Id);
            var examResultDTO = _mapper.Map<GetExamResultForUpdateDTO>(examResult);
            return examResultDTO;
        }
        public async Task CreateExamResultAsync(PostExamResultDTO postExamResultDTO)
        {
            var exam = await _examRepository.GetSingleAsync(e => e.Id == postExamResultDTO.ExamId,"GroupSubject.Group.Students");
            

            if (exam is null)
                throw new ExamNotFoundByIdException("Exam not found");
            if (postExamResultDTO.Score > exam.MaxScore || postExamResultDTO.Score < 0)
                throw new ExamResultScoreCannotBeMoreThanMaxScoreException("Invalid value for score");

            var student = await _studentRepository.GetSingleAsync(s=>s.Id == postExamResultDTO.StudentId);
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");
            if (await _examResultRepository.IsExistsAsync(er => er.Exam.Id == postExamResultDTO.ExamId && er.StudentId == postExamResultDTO.StudentId))
                throw new ExamResultAlreadyExistsException("Student already has result of this exam");
            if(!exam.GroupSubject.Group.Students.Any(s=>s.Id == postExamResultDTO.StudentId))
            {
                throw new StudentDoNotHasThisExamException("Student do not has this exam");
            }
            


            var newExamResult = _mapper.Map<ExamResult>(postExamResultDTO);
           await _examResultRepository.CreateAsync(newExamResult);
           await _examResultRepository.SaveChangesAsync();


        }
        public async Task UpdateExamResultAsync(Guid id, PutExamResultDTO putExamResultDTO)
        {
            var examResult = await _examResultRepository.GetSingleAsync(e => e.Id == id,"Student","Exam");
            if (examResult is null)
                throw new ExamResultNotFoundByIdException("Exam's result not found");
            var exam = await _examRepository.GetSingleAsync(e=>e.Id == putExamResultDTO.ExamId,"GroupSubject.Group.Students");


            if (exam is null)
                throw new ExamNotFoundByIdException("Exam not found");

            if (putExamResultDTO.Score > exam.MaxScore || putExamResultDTO.Score < 0)
                throw new ExamResultScoreCannotBeMoreThanMaxScoreException("Invalid value for score");

            var student = await _studentRepository.GetSingleAsync(s=>s.Id == putExamResultDTO.StudentId);

            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            if (await _examResultRepository.IsExistsAsync(er => er.Exam.Id == putExamResultDTO.ExamId && er.StudentId == putExamResultDTO.StudentId) && examResult.ExamId != putExamResultDTO.ExamId) 
                throw new ExamResultAlreadyExistsException("Student already has result of this exam");  

            if (!exam.GroupSubject.Group.Students.Any(s => s.Id == putExamResultDTO.StudentId))
            {
                throw new StudentDoNotHasThisExamException("Student do not has this exam");
            }



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
