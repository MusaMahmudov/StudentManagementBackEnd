using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ISubjectHourService
    {
        Task CreateSubjectHourAsync(PostSubjectHourDTO postSubjectHourDTO);
    }
}
