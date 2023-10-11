using AutoMapper;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class SubjectMapper : Profile
    {
        public SubjectMapper() 
        {
            CreateMap<Subject, GetSubjectDTO>().ReverseMap();
            CreateMap<PostSubjectDTO,Subject>().ReverseMap();
            CreateMap<PutSubjectDTO, Subject>().ReverseMap();

        }
    }
}
