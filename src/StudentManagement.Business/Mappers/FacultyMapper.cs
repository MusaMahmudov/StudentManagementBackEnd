using AutoMapper;
using StudentManagement.Business.DTOs.FacultyDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class FacultyMapper : Profile
    {
        public FacultyMapper() 
        {
            CreateMap<Faculty,GetFacultyDTO>().ReverseMap();
            CreateMap<PostFacultyDTO, Faculty>().ReverseMap();
            CreateMap<PutFacultyDTO, Faculty>().ReverseMap();
            CreateMap<Faculty, GetFacultyForUpdateDTO>().ReverseMap();

        }
    }
}
