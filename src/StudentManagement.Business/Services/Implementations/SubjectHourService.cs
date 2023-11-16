using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
using StudentManagement.Business.Exceptions.LessonTypeExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.SubjectHourExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace StudentManagement.Business.Services.Implementations
{
    public class SubjectHourService : ISubjectHourService
    {
        private readonly ISubjectHourRepository _subjectHourRepository;
        private readonly IMapper _mapper;
        private readonly ILessonTypeRepository _lessonTypeRepository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentRepository _studentRepository;
        public SubjectHourService(IStudentRepository studentRepository,IAttendanceRepository attendanceRepository,ISubjectHourRepository subjectHourRepository,IMapper mapper, ILessonTypeRepository lessonTypeRepository, IGroupSubjectRepository groupSubjectRepository)
        {
            _studentRepository = studentRepository;
            _attendanceRepository = attendanceRepository;
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

            var groupSubject = await _groupSubjectRepository.GetSingleAsync(gs=>gs.Id == postSubjectHourDTO.GroupSubjectId, "Group.Students");
            var subjectHours = new List<SubjectHour>();
            var startTime = new TimeSpan(postSubjectHourDTO.StartTime.Hours,postSubjectHourDTO.StartTime.Minutes,postSubjectHourDTO.StartTime.Seconds);
            var endTime = new TimeSpan(postSubjectHourDTO.EndTime.Hours, postSubjectHourDTO.EndTime.Minutes, postSubjectHourDTO.EndTime.Seconds);
            DateTime startDate = new DateTime();

            switch (groupSubject.Semester)
            {
                case "Payiz":
                    startDate = new DateTime(groupSubject.Year,9,15);
                   break;
                case "Yaz":
                    startDate = new DateTime(groupSubject.Year, 2, 15);
                        break;
                case "Yay":
                    startDate = new DateTime(groupSubject.Year,7,15);
                    break;

            }


            DateTime firstLesson = startDate;
            while (firstLesson.DayOfWeek != postSubjectHourDTO.DayOfWeek)
            {
                firstLesson = firstLesson.AddDays(1);
            }

            for ( var i = 0;i<groupSubject.TotalWeeks;i++)
            {
                
                var subjectHour = _mapper.Map<SubjectHour>(postSubjectHourDTO);
                subjectHour.StartTime = startTime;
                subjectHour.EndTime = endTime;
                subjectHour.Date = firstLesson.AddDays(7 * i);
                subjectHours.Add(subjectHour);
                foreach (var student in groupSubject.Group.Students)
                {
                    var newAttendance = new Attendance()
                    {
                        StudentId = student.Id,
                        SubjectHour = subjectHour,
                    };

                    await _attendanceRepository.CreateAsync(newAttendance);
                }
            }
            _subjectHourRepository.AddList(subjectHours);
            await _subjectHourRepository.SaveChangesAsync();
        }
        public async Task<GetSubjectHourDTO> GetSubjectHourByIdAsync(Guid Id)
        {
            var subjectHour = await _subjectHourRepository.GetSingleAsync(sh => sh.Id == Id, "GroupSubject.Group", "GroupSubject.Subject", "LessonType", "GroupSubject.teacherSubjects.TeacherRole", "GroupSubject.teacherSubjects.Teacher");

            if (subjectHour is null)
            {
                throw new SubjectHourNotFoundByIdException("Subject Hour Not Found");
            }
            var subjectHourDTO = _mapper.Map<GetSubjectHourDTO>(subjectHour);
            return subjectHourDTO;
        }
        public async Task<List<GetSubjectHourForStudentScheduleDTO>> GetSubjectHoursForStudentScheduleAsync(Guid studentId)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == studentId,"Group.GroupSubjects", "studentGroups.Group.GroupSubjects");
            if(student is null)
            {
                throw new StudentNotFoundByIdException("Student not found");
            }


            List<SubjectHour> subjectHours = new List<SubjectHour>();
            if(student.Group is not null)
            {
                foreach (var groupSubjectId in student.Group.GroupSubjects.Select(gs => gs.Id))
                {
                    var groupSubjectHours = await _subjectHourRepository.GetFiltered(sh => sh.GroupSubjectId == groupSubjectId, "LessonType", "GroupSubject.Subject", "GroupSubject.Group").ToListAsync();
                    foreach (var subjectHour in groupSubjectHours)
                    {
                        subjectHours.Add(subjectHour);
                    }
                }
            }
            //if(student.studentGroups.Count() > 0)
            //{
            //    foreach(var studentGroup in student.studentGroups)
            //    {
            //        if(studentGroup.Group?.GroupSubjects.Count() > 0)
            //        {
            //            foreach (var groupSubjectId in studentGroup.Group.GroupSubjects.Select(gs => gs.Id))
            //            {
            //                var groupSubjectHours = await _subjectHourRepository.GetFiltered(sh => sh.GroupSubjectId == groupSubjectId, "LessonType", "GroupSubject.Subject", "GroupSubject.Group").ToListAsync();
            //                foreach (var subjectHour in groupSubjectHours)
            //                {
            //                    subjectHours.Add(subjectHour);
            //                }
            //            }
            //        }
                  
            //    }
               
            //}
           

            var subjectHoursDTO = _mapper.Map<List<GetSubjectHourForStudentScheduleDTO>>(subjectHours);
            return subjectHoursDTO;

        }
        public async Task<List<GetSubjectHourForTeacherScheduleDTO>> GetSubjectHoursForTeacherScheduleAsync(Guid teacherId)
        {
            
                var subjectHours = await _subjectHourRepository.GetFiltered(sh => sh.GroupSubject.teacherSubjects.Any(ts=>ts.TeacherId == teacherId), "LessonType", "GroupSubject.Subject", "GroupSubject.teacherSubjects.Teacher", "GroupSubject.teacherSubjects.TeacherRole", "GroupSubject.Group").ToListAsync();
              

            var subjectHoursDTO = _mapper.Map<List<GetSubjectHourForTeacherScheduleDTO>>(subjectHours);
            return subjectHoursDTO;
        }
        public async Task<List<GetSubjectHourDTO>> GetSubjectHourAsync(Guid? groupSubjectId)
        {
            var subjectHours = await _subjectHourRepository.GetFiltered(sh => groupSubjectId != null ? sh.GroupSubjectId == groupSubjectId : true , "LessonType", "GroupSubject.Subject", "GroupSubject.Group", "GroupSubject.teacherSubjects.Teacher", "GroupSubject.teacherSubjects.TeacherRole").ToListAsync();
                
            

            var subjectHoursDTO = _mapper.Map<List<GetSubjectHourDTO>>(subjectHours);
            return subjectHoursDTO;

        }

        public async Task UpdateSubjeectHoursAsync(Guid Id,PutSubjectHourDTO putSubjectHourDTO)
        {
            var subjectHour = await _subjectHourRepository.GetSingleAsync(sh => sh.Id == Id, "GroupSubject","LessonType");
            if(subjectHour is null)
            {
                throw new SubjectHourNotFoundByIdException("Subject's hour not found");
            }
            var groupSubject = await _groupSubjectRepository.GetSingleAsync(gs => gs.Id == putSubjectHourDTO.GroupSubjectId);
            if(groupSubject is null)
            {
                throw new GroupSubjectNotFoundByIdException("Group Subject Not Found");
            }
            var existingSubjectHours = _subjectHourRepository.GetFiltered(sh =>sh.GroupSubjectId == subjectHour.GroupSubjectId && sh.LessonTypeId == subjectHour.LessonTypeId && sh.StartTime == subjectHour.StartTime && sh.DayOfWeek == subjectHour.DayOfWeek ).ToList();

            var startTime = new TimeSpan(putSubjectHourDTO.StartTime.Hours, putSubjectHourDTO.StartTime.Minutes, putSubjectHourDTO.StartTime.Seconds);
            var endTime = new TimeSpan(putSubjectHourDTO.EndTime.Hours, putSubjectHourDTO.EndTime.Minutes, putSubjectHourDTO.EndTime.Seconds);
            DateTime startDate = new DateTime();



            switch (groupSubject.Semester)
            {
                case "Payiz":
                    startDate = new DateTime(groupSubject.Year, 9, 15);
                    break;
                case "Yaz":
                    startDate = new DateTime(groupSubject.Year, 2, 15);
                    break;
                case "Yay":
                    startDate = new DateTime(groupSubject.Year, 7, 15);
                    break;

            }
            DateTime firstLesson = startDate;

            while (firstLesson.DayOfWeek != putSubjectHourDTO.DayOfWeek)
            {
                firstLesson = firstLesson.AddDays(1);
            }



            for (var i = 0; i < existingSubjectHours.Count(); i++)
            {

                existingSubjectHours[i] = _mapper.Map(putSubjectHourDTO, existingSubjectHours[i]);
                existingSubjectHours[i].StartTime = startTime;
                existingSubjectHours[i].EndTime = endTime;
                existingSubjectHours[i].Date = firstLesson.AddDays(7 * i);
                _subjectHourRepository.Update(existingSubjectHours[i]);
            }
           await _subjectHourRepository.SaveChangesAsync();
        }

        public async Task<GetSubjectHourForUpdateDTO> GetSubjectHourForUpdateAsync(Guid? subjectHourId)
        {
          var subjectHour = await _subjectHourRepository.GetSingleAsync(sh => sh.Id == subjectHourId);
            if (subjectHour is null) 
            {
                throw new SubjectHourNotFoundByIdException("Subject Hour Not Found");
            }
            var subjectHourDTO = _mapper.Map<GetSubjectHourForUpdateDTO>(subjectHour);
            return subjectHourDTO;

        }

        public async Task DeleteSubjectHoursAsync(Guid Id)
        {
           var subjectHour = await _subjectHourRepository.GetSingleAsync(sh=>sh.Id == Id,"GroupSubject","LessonType");
            if(subjectHour is null)
            {
                throw new SubjectHourNotFoundByIdException("Subject Hour Not Found");
            }
            var subjectHours = _subjectHourRepository.GetFiltered(sh => sh.GroupSubjectId == subjectHour.GroupSubjectId && sh.LessonTypeId == subjectHour.LessonTypeId && sh.StartTime == subjectHour.StartTime && sh.DayOfWeek == subjectHour.DayOfWeek ).ToList();
            _subjectHourRepository.DeleteList(subjectHours);
           await  _subjectHourRepository.SaveChangesAsync();
        }

        public async Task<List<GetSubjectHourForAttendanceForTeacherPageDTO>> GetSubjectHoursForAttendanceForTeacherPageAsync(Guid groupSubjectId)
        {
           var subjectHours =  await _subjectHourRepository.GetFiltered(sh=>sh.GroupSubjectId == groupSubjectId, "LessonType","Attendances.Student").OrderBy(s=>s.Date).ThenBy(s=>s.StartTime).ToListAsync();
            var subjectHoursDTO = _mapper.Map<List<GetSubjectHourForAttendanceForTeacherPageDTO>>(subjectHours);

            return subjectHoursDTO;
        }





        //public Task<List<GetSubjectHourForStudentAttendancePageDTO>> GetSubjectHourForAttendancePageAsync(Guid groupSubjectId, Guid stundentId)
        //{
        //    //var subjectHours = _subjectHourRepository.GetFiltered(sh=>sh.GroupSubjectId == groupSubjectId && sh.Attendances.Select(a=>a.StudentId == stundentId)).ToList();
        //}
    }
}
