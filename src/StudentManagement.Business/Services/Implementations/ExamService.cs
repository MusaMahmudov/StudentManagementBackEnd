using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.Exceptions.ExamExceptions;
using StudentManagement.Business.Exceptions.ExamTypeExceptions;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
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
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IExamTypeRepository _examTypeRepository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IExamResultRepository _examResultRepository;
        public ExamService(IExamResultRepository examResultRepository,ITeacherRepository teacherRepository,IStudentRepository studentRepository,IExamRepository examRepository,IMapper mapper,IGroupSubjectRepository groupSubjectRepository,IExamTypeRepository examTypeRepository) 
        {
            _examResultRepository = examResultRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
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
        public async Task<GetExamForExamsForTeacherPageAssign> GetExamForExamsForTeacherPageAssignAsync(Guid id)
        {
            var exam = await _examRepository.GetSingleAsync(e => e.Id == id);
            var examDTO = _mapper.Map<GetExamForExamsForTeacherPageAssign>(exam);
            return examDTO;
        }
        public async Task<List<GetExamForSubjectsForStudentPageDTO>> GetExamsForSubjectsForStudentPageAsync(Guid groupSubjectId)
        {
           var exams = await _examRepository.GetFiltered(e=>e.GroupSubjectId == groupSubjectId,"ExamResults.Student","ExamType").ToListAsync();
            var examsDTO = _mapper.Map<List<GetExamForSubjectsForStudentPageDTO>>(exams);
            return examsDTO;
        }
        public async Task<List<GetExamsForExamResultUpdateDTO>> GetAllExamsForExamResultUpdateAsync()
        {
           var exams = await _examRepository.GetAll("ExamType","GroupSubject.Group", "GroupSubject.Subject").ToListAsync();
            var examsDTO = _mapper.Map<List<GetExamsForExamResultUpdateDTO>>(exams);
            return examsDTO;
        }
        public async Task CreateExamAsync(PostExamDTO postExamDTO)
        {
            var groupSubject = await _groupSubjectRepository.GetSingleAsync(gs=>gs.Id == postExamDTO.GroupSubjectId);


            if (groupSubject is null)
                throw new GroupSubjectNotFoundByIdException("group's subject not found");

            var examType = await _examTypeRepository.GetSingleAsync(et=>et.Id == postExamDTO.ExamTypeId);
            if(examType is null)
                throw new ExamTypeNotFoundByIdException("Exam's type not found");

            if(await _examRepository.IsExistsAsync(e=>e.GroupSubjectId == groupSubject.Id && e.ExamTypeId == examType.Id))
                throw new ExamAlreadyExistsEception("Exam already exists ");

            if(postExamDTO.Date <= DateTime.Now)
            {
                throw new ExamAlreadyExistsEception("The exam is scheduled for an early date");

            }


            var newExam = _mapper.Map<Exam>(postExamDTO);
            
            
           await _examRepository.CreateAsync(newExam);
          await  _examRepository.SaveChangesAsync();

        }
        public async Task UpdateExamAsync(Guid id, PutExamDTO putExamDTO)
        {
            var Exam = await _examRepository.GetSingleAsync(e => e.Id == id, "ExamType", "GroupSubject.Group", "GroupSubject.Subject");
            if (Exam is null)
                throw new ExamNotFoundByIdException("Exam not found");
            if(Exam.ExamTypeId != putExamDTO.ExamTypeId)
            {
                if (!await _examTypeRepository.IsExistsAsync(et => et.Id == putExamDTO.ExamTypeId))
                    throw new ExamTypeNotFoundByIdException("Exam type not found");
            }
            if(Exam.GroupSubjectId != putExamDTO.GroupSubjectId || Exam.ExamTypeId != putExamDTO.ExamTypeId)
            {
                if (!await _groupSubjectRepository.IsExistsAsync(gs => gs.Id == putExamDTO.GroupSubjectId))
                    throw new GroupSubjectNotFoundByIdException("Group subject not found");

                if(putExamDTO.GroupSubjectId != Exam.GroupSubjectId || putExamDTO.ExamTypeId != Exam.ExamTypeId)
                {
                    if (await _examRepository.IsExistsAsync(e => e.GroupSubjectId == putExamDTO.GroupSubjectId && e.ExamTypeId == putExamDTO.ExamTypeId))
                    {
                        throw new ExamAlreadyExistsEception("Exam already exists ");
                    }

                }

            }
            if(putExamDTO.maxScore != Exam.MaxScore)
            {
                var oldExamResults = await _examResultRepository.GetFiltered(er=>er.ExamId == Exam.Id).ToListAsync();
                if(oldExamResults.Count()> 0)
                {
                    foreach(var examResult in oldExamResults)
                    {
                        if(examResult.Score > putExamDTO.maxScore)
                        {
                            _examResultRepository.Delete(examResult);
                        }
                    }

                }
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

        public async Task<List<GetExamForExamsScheduleForUserPage>> GetExamsForExamScheduleForStudentPageAsync(Guid studentId)
        {
            var student = await _studentRepository.GetSingleAsync(s=>s.Id == studentId,"Group");
            if(student is null)
            {
                throw new StudentNotFoundByIdException("Student not found");
            }

          var exams = await  _examRepository.GetFiltered(e => e.GroupSubject.GroupId == student.GroupId, "ExamType","GroupSubject.Group", "GroupSubject.Subject").ToListAsync();
            var examsDTO = _mapper.Map<List<GetExamForExamsScheduleForUserPage>>(exams);
            return examsDTO;
        }

        public async Task<List<GetExamForExamsScheduleForUserPage>> GetExamsForExamScheduleForTeacherPageAsync(Guid teacherId)
        {
            if(!await _teacherRepository.IsExistsAsync(t=>t.Id == teacherId))
            {
                throw new TeacherNotFoundByIdException("Teacher Not Found");
            }
           var exams =await _examRepository.GetFiltered(e=>e.GroupSubject.teacherSubjects.Any(ts=>ts.TeacherId == teacherId),"GroupSubject.Group", "GroupSubject.Subject","ExamType").ToListAsync();
            var examsDTO = _mapper.Map<List<GetExamForExamsScheduleForUserPage>>(exams);
            return examsDTO;

        }

        
    }
}
