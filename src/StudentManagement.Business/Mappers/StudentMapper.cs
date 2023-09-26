using AutoMapper;
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
          CreateMap<Student,GetStudentDTO>().ReverseMap();
          CreateMap<PostStudentDTO,Student>().ReverseMap();
        }
    }
}
