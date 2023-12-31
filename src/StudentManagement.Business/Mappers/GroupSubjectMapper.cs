﻿using AutoMapper;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class GroupSubjectMapper : Profile
    {
        public GroupSubjectMapper() 
        {
            CreateMap<GroupSubject,GetGroupSubjectForExamUpdateDTO>().ForMember(ggs=>ggs.SubjectName,x=>x.MapFrom(gs=>gs.Subject.Name)).ForMember(ggs=>ggs.GroupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();

            CreateMap<GroupSubject,GetGroupSubjectForExamForExamsSchedule>().ForMember(ggs => ggs.SubjectName, x => x.MapFrom(gs => gs.Subject.Name)).ForMember(ggs=>ggs.GroupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();
            CreateMap<GroupSubject, GetGroupSubjectForSubjectsForStudentPageDTO>().ForMember(ggs => ggs.TeacherRoles, x => x.MapFrom(gs => gs.teacherSubjects)).ForMember(gg => gg.groupName, x => x.MapFrom(gs => gs.Group.Name)).ReverseMap(); CreateMap<GroupSubject,GetGroupSubjectForExamResult>().ForMember(gg=>gg.groupName,x=>x.MapFrom(gs=>gs.Group.Name)).ForMember(gg=>gg.subjectName,x=>x.MapFrom(gs=>gs.Subject.Name)).ReverseMap();
            CreateMap<GroupSubject,GetGroupSubjectDTO>().ForMember(gg=>gg.teachers,x=>x.MapFrom(g=>g.teacherSubjects.Select(ts=> new GetTeacherForGroupSubjectDTO { Id = ts.TeacherId,FullName = ts.Teacher.FullName , RoleName = ts.TeacherRole.Name}))).ReverseMap();
            CreateMap<PostGroupSubjectDTO, GroupSubject>().ForMember(gs=>gs.teacherSubjects,x=>x.Ignore()).ReverseMap();
            CreateMap<PutGroupSubjectDTO, GroupSubject>().ForMember(gs => gs.teacherSubjects, x => x.Ignore()).ReverseMap();
                CreateMap<GroupSubject,GetGroupSubjectForTeacherDTO>().ReverseMap();
            CreateMap<GroupSubject,GetGroupSubjectForStudentScheduleDTO>().ForMember(gg=>gg.subjectName,x=>x.MapFrom(gs=>gs.Subject.Name)).ForMember(gg=>gg.groupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();
            CreateMap<GroupSubject, GetGroupSubjectForTeacherScheduleDTO>().ForMember(gg => gg.subjectName, x => x.MapFrom(gs => gs.Subject.Name)).ForMember(gg => gg.groupName, x => x.MapFrom(gs => gs.Group.Name)).ReverseMap();
            CreateMap<GroupSubject,GetGroupSubjectForTeacherPageDTO>().ForMember(ggs=>ggs.groupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();
            CreateMap<GroupSubject, GetGroupSubjectForGroupDTO>().ForMember(ggs=>ggs.TeacherRoles,x=>x.MapFrom(gs=>gs.teacherSubjects)).ForMember(gg=>gg.groupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();
            //CreateMap<GroupSubject, GetGroupSubjectForGroupDTO>().ForMember(ggs=>ggs.TeacherRoles,x=>x.MapFrom(gs=>gs.teacherSubjects.Select(ts => new GetTeacherSubjectForGroupDTO { Id = ts.Id, teacherRole = ts.TeacherRole.Name, teacherName = ts.Teacher.FullName }))).ReverseMap();
            CreateMap<GroupSubject, GetGroupSubjectForExam>().ForMember(gg=>gg.subjectName,x=>x.MapFrom(gs=>gs.Subject.Name)).ForMember(gg=>gg.groupName,x=>x.MapFrom(gs=>gs.Group.Name)).ReverseMap();
            CreateMap<GroupSubject,GetGroupSubjectForUpdateDTO>().ForMember(ggs => ggs.teacherRole,x=>x.Ignore()).ReverseMap();
        }
    }
}
