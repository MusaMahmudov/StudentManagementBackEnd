using AutoMapper;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Mappers
{
    public class SubjectHourMappers : Profile
    {
        public SubjectHourMappers() 
        {
            CreateMap<PostSubjectHourDTO, SubjectHour>().ForMember(sh => sh.StartTime, x => x.Ignore()).ForMember(sh=>sh.EndTime,x=>x.Ignore());
        }

    }
}
