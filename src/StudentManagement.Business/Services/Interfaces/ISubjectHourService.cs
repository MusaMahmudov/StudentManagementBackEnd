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
        Task CreateSubjectHoursAsync(PostSubjectHourDTO postSubjectHourDTO);
        Task<List<GetSubjectHourForStudentScheduleDTO>> GetSubjectHoursForStudentScheduleAsync(Guid studentId);
        Task<List<GetSubjectHourForTeacherScheduleDTO>> GetSubjectHoursForTeacherScheduleAsync(Guid teacherId);

        Task<List<GetSubjectHourDTO>> GetSubjectHourAsync(Guid? groupSubjectId);
        Task <GetSubjectHourDTO> GetSubjectHourByIdAsync(Guid Id);
        Task<List<GetSubjectHourForAttendanceForTeacherPageDTO>> GetSubjectHoursForAttendanceForTeacherPageAsync(Guid groupSubjectId);


        //Task <List<GetSubjectHourForStudentAttendancePageDTO>> GetSubjectHourForAttendancePageAsync(Guid groupSubjectId,Guid stundentId);
        Task<GetSubjectHourForUpdateDTO> GetSubjectHourForUpdateAsync(Guid? subjectHourId);
        Task UpdateSubjeectHoursAsync(Guid Id,PutSubjectHourDTO putSubjectHourDTO);
        Task DeleteSubjectHoursAsync(Guid Id);
        
    }
}
