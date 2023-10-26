﻿using AutoMapper;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class TeacherSubjectMapper : Profile
    {
        public TeacherSubjectMapper() 
        {
            CreateMap<TeacherSubject, GetTeacherSubjectForGroupDTO>().ForMember(gt => gt.teacherRole, x => x.MapFrom(ts => ts.TeacherRole.Name)).ForMember(gt => gt.teacherName,x=>x.MapFrom(ts=>ts.Teacher.FullName)).ReverseMap();
            CreateMap<PostTeacherSubjectRoleDTO, TeacherSubject>().ReverseMap();
            CreateMap<TeacherSubject, GetTeacherSubjectForTeacherDTO>().ForMember(gt => gt.TeacherRoleName, x => x.MapFrom(ts => ts.TeacherRole.Name)).ReverseMap();
            CreateMap<TeacherSubject, GetTeacherSubjectForSubjectHourForStudentScheduleDTO>().ForMember(gt=>gt.teacherName,x=>x.MapFrom(ts=>ts.Teacher.FullName)).ForMember(gt=>gt.teacherRoleName,x=>x.MapFrom(ts=>ts.TeacherRole.Name)).ReverseMap();
        }
    }
}
