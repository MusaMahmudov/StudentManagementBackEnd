using AutoMapper;
using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class ExamMappers : Profile
    {
        public ExamMappers() 
        {
            CreateMap<Exam, GetExamsForExamResultUpdateDTO>().ForMember(ge => ge.examTypeName, x => x.MapFrom(e => e.ExamType.Name)).ForMember(ge => ge.groupName, x => x.MapFrom(e => e.GroupSubject.Group.Name)).ForMember(ge => ge.subjectName, x => x.MapFrom(e => e.GroupSubject.Subject.Name)).ReverseMap();

            CreateMap<Exam,GetExamForExamsForTeacherPageAssign>().ReverseMap();
            CreateMap<Exam,GetExamForExamsScheduleForUserPage>().ForMember(ge=>ge.ExamTypeName,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
            CreateMap<Exam,GetExamForTeacherPageDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
            CreateMap<Exam,GetExamForExamResultDTO>().ForMember(ge=>ge.ExamTypeName,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
           CreateMap<Exam,GetExamDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
            CreateMap<Exam, GetExamForUpdateDTO>().ForMember(ge => ge.ExamTypeId, x => x.MapFrom(e => e.ExamType.Id));
            CreateMap<PostExamDTO,Exam>().ReverseMap();
            CreateMap<PutExamDTO,Exam>().ReverseMap();

            CreateMap<Exam,GetExamForExamResultForStudentPageDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();

            CreateMap<Exam,GetExamForSubjectsForStudentPageDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ReverseMap();
            CreateMap<Exam, GetExamForStudentPageDTO>().ForMember(ge=>ge.ExamType,x=>x.MapFrom(e=>e.ExamType.Name)).ForMember(ge=>ge.examResults,x=>x.MapFrom(e=>e.ExamResults.Select(er=>new GetExamResultForExamForStudentPageDTO{studentId= er.StudentId,studentName = er.Student.FullName,score = er.Score   }))).ReverseMap();
        }
    }
}
