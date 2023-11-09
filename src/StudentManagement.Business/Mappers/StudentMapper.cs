using AutoMapper;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper() 
        {
            CreateMap<Student, StudentForTokenDTO>().ReverseMap();

            CreateMap<Student,GetStudentForCreateOrUpdateForExamResultDTO>().ReverseMap();
            CreateMap<Student,GetStudentForGroupForDetailsDTO>().ReverseMap();

            CreateMap<Student,GetStudentForGroupUpdateDTO>().ReverseMap();

            CreateMap<Student,GetStudentForAttendanceForTeacherPageDTO>().ReverseMap();
            CreateMap<Student, GetStudentForExamsForTeacherPageDTO>().ReverseMap();
            CreateMap<Student,GetStudentForStudentAttendancePageDTO>().ReverseMap();
            CreateMap<Student, GetStudentForUser>().ForMember(gt => gt.studentName, x => x.MapFrom(t => t.FullName)).ReverseMap();
            CreateMap<Student, GetStudentForStudentPageDTO>().ForMember(gs=>gs.MainGroup,x=>x.MapFrom(s=>s.Group)).ReverseMap();
            CreateMap<Student,GetStudentForGroupForStudentPageDTO>().ReverseMap();
            CreateMap<Student,GetStudentDTO>().ForMember(gs=>gs.MainGroup,x=>x.MapFrom(s=>s.Group)).ForMember(gt=>gt.examResults,x=>x.MapFrom(s=>s.examResults)).ForMember(gs=>gs.Groups,x=>x.MapFrom(s=>s.studentGroups.Select(sg => new GetGroupStudentDTO {Id = sg.GroupId,Name = sg.Group.Name }))).ReverseMap();
          //CreateMap<PostStudentDTO,Student>().ForMember(s=>s.studentGroups,x=>x.MapFrom(ps=>ps.SubGroupsId.Select(sg=> new StudentGroup { GroupId = sg}))).ForMember(s=>s.GroupId,x=>x.MapFrom(ps=>ps.MainGroup)).ReverseMap(); //если будут подгруппы
            CreateMap<PostStudentDTO, Student>().ForMember(s => s.GroupId, x => x.MapFrom(ps => ps.MainGroup)).ReverseMap();
            CreateMap<PutStudentDTO, Student>().ReverseMap();
            CreateMap<Student, GetStudentForUpdateDTO>().ForMember(gt => gt.AppUserId, x => x.MapFrom(s => s.AppUserId));
            //CreateMap<Student, GetStudentForUpdateDTO>().ForMember(gt=>gt.GroupId,x=>x.MapFrom(s=>s.studentGroups.Select(sg=>sg.GroupId))).ForMember(gt=>gt.MainGroup,x=>x.MapFrom(S=>S.GroupId)).ForMember(gt=>gt.AppUserId,x=>x.MapFrom(s=>s.AppUserId));

        }
    }
}
