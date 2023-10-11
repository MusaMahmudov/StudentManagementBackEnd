using AutoMapper;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.StudentDTOs;
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
          CreateMap<Student,GetStudentDTO>().ForMember(gs=>gs.MainGroup,x=>x.MapFrom(s=>s.Group)).ForMember(gt=>gt.examResults,x=>x.MapFrom(s=>s.examResults)).ForMember(gs=>gs.Groups,x=>x.MapFrom(s=>s.studentGroups.Select(sg => new GetGroupStudentDTO {Id = sg.GroupId,Name = sg.Group.Name }))).ReverseMap();
          CreateMap<PostStudentDTO,Student>().ForMember(s=>s.GroupId,x=>x.MapFrom(ps=>ps.MainGroup)).ReverseMap();
            CreateMap<PutStudentDTO, Student>().ForMember(s=>s.GroupId,x=>x.MapFrom(ps=>ps.MainGroup)).ReverseMap();

        }
    }
}
