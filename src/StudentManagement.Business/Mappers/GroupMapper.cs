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
    public class GroupMapper : Profile
    {
        public GroupMapper() 
        {
            CreateMap<Group,GetGroupForGroupSubjectDTO>().ForMember(gg=>gg.FacultyName,x=>x.MapFrom(g=>g.Faculty.Name)).ReverseMap();
            CreateMap<Group,GetGroupForTeacherDTO>().ReverseMap();
            CreateMap<Group, GetMainGroupForStudentDTO>().ForMember(gg=>gg.facultyName,x=>x.MapFrom(g=>g.Faculty.Name)).ReverseMap();

            CreateMap<Group, GetGroupDTO>().ForMember(gg => gg.FacultyName, x => x.MapFrom(g=>g.Faculty.Name)).ForMember(gg=>gg.Students,x=>x.MapFrom(g=>g.studentGroups.Select(sg=> new GetStudentGroupDTO{FullName = sg.Student.FullName,Id = sg.Student.Id }))).ReverseMap();
            CreateMap<PostGroupDTO, Group>().ForMember(g => g.FacultyId, x => x.MapFrom(pg => pg.FacultyId)).ForMember(g=>g.studentGroups,x=>x.MapFrom(pg=>pg.StudentsId.Select(Id => new StudentGroup {StudentId = Id }))).ReverseMap();

        }
    }
}
