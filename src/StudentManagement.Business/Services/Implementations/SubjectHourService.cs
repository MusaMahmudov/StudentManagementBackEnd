using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
using StudentManagement.Business.Exceptions.LessonTypeExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class SubjectHourService : ISubjectHourService
    {
        private readonly ISubjectHourRepository _subjectHourRepository;
        private readonly IMapper _mapper;
        private readonly ILessonTypeRepository _lessonTypeRepository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        public SubjectHourService(ISubjectHourRepository subjectHourRepository,IMapper mapper, ILessonTypeRepository lessonTypeRepository, IGroupSubjectRepository groupSubjectRepository)
        {
            _mapper = mapper;
            _subjectHourRepository = subjectHourRepository;
            _lessonTypeRepository = lessonTypeRepository;
            _groupSubjectRepository = groupSubjectRepository;
        }
        public async Task CreateSubjectHoursAsync(PostSubjectHourDTO postSubjectHourDTO)
        {
            if (!await _groupSubjectRepository.IsExistsAsync(gs => gs.Id == postSubjectHourDTO.GroupSubjectId))
                throw new GroupSubjectNotFoundByIdException("Group's subject not found");
            if (!await _lessonTypeRepository.IsExistsAsync(lt => lt.Id == postSubjectHourDTO.LessonTypeId))
                throw new LessonTypeNotFoundByIdException("Lesson's type not found");

            var groupSubject = await _groupSubjectRepository.GetSingleAsync(gs=>gs.Id == postSubjectHourDTO.GroupSubjectId);
            var subjectHours = new List<SubjectHour>();
            var startTime = new TimeSpan(postSubjectHourDTO.StartTime.Hours,postSubjectHourDTO.StartTime.Minutes,postSubjectHourDTO.StartTime.Seconds);
            var endTime = new TimeSpan(postSubjectHourDTO.EndTime.Hours, postSubjectHourDTO.EndTime.Minutes, postSubjectHourDTO.EndTime.Seconds);

            for ( var i = 0;i<groupSubject.TotalWeeks;i++)
            {
                
                var subjectHour = _mapper.Map<SubjectHour>(postSubjectHourDTO);
                subjectHour.StartTime = startTime;
                subjectHour.EndTime = endTime;
                subjectHour.Date = postSubjectHourDTO.StartDate.AddDays(7 * i);
                subjectHours.Add(subjectHour);
            }
            _subjectHourRepository.AddList(subjectHours);
            await _subjectHourRepository.SaveChangesAsync();
        }
        public async Task<List<GetSubjectHourForStudentScheduleDTO>> GetSubjectHoursForStudentScheduleAsync(List<Guid> groupSubjecstId)
        {
            List<SubjectHour> subjectHours = new List<SubjectHour>();
            foreach (var groupSubjectId in groupSubjecstId)
            {
                var groupSubjectHours = await _subjectHourRepository.GetFiltered(sh => sh.GroupSubjectId == groupSubjectId, "LessonType","GroupSubject.Subject", "GroupSubject.Group").ToListAsync();
                foreach (var subjectHour in groupSubjectHours)
                {
                    subjectHours.Add(subjectHour);
                }
            }

            var subjectHoursDTO = _mapper.Map<List<GetSubjectHourForStudentScheduleDTO>>(subjectHours);
            return subjectHoursDTO;

        }

    }
}
