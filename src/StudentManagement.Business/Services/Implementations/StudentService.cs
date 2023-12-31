﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.Exceptions.GroupExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.TeacherExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Enums;
using StudentManagement.DataAccess.Migrations;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentGroupRepository _studentGroupRepository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        public StudentService(IGroupSubjectRepository groupSubjectRepository,UserManager<AppUser> userManager,IStudentRepository studentRepository, IMapper mapper,AppDbContext context,IGroupRepository groupRepository,IGroupSubjectService groupSubjectService,IStudentGroupRepository studentGroupRepository)
        {
            _groupSubjectRepository = groupSubjectRepository;
            _userManager = userManager;
            _studentRepository = studentRepository;
           _groupRepository = groupRepository;
            _mapper = mapper;
            _studentGroupRepository = studentGroupRepository;
        }

        public async Task<List<GetStudentDTO>> GetAllStudentsAsync(string? search)
        {
            var students = await _studentRepository.GetFiltered(s => search != null ? s.FullName.Contains(search) : true,"studentGroups.Group.Faculty","Group","AppUser","examResults.Exam.ExamType", "examResults.Exam.GroupSubject.Subject","Group.Faculty").ToListAsync();

            return _mapper.Map<List<GetStudentDTO>>(students);
        }

        public async Task<GetStudentDTO> GetStudentByIdAsync(Guid Id)
        {

            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id, "studentGroups.Group.Faculty", "AppUser", "examResults.Exam.ExamType","Group.Faculty", "examResults.Exam.GroupSubject.Subject");
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            return _mapper.Map<GetStudentDTO>(student);
        }
        public async Task<GetStudentForUpdateDTO> GetStudentByIdForUpdateAsync(Guid Id)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id, "studentGroups.Group.Faculty", "AppUser", "examResults.Exam.ExamType", "examResults.Exam.GroupSubject.Subject");
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            return _mapper.Map<GetStudentForUpdateDTO>(student);
        }
        public async Task<GetStudentForStudentPageDTO> GetStudentForStudentPageAsync(Guid Id)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id,"Group.GroupSubjects.subjectHours", "studentGroups.Group.Faculty", "Group.Faculty","Group.Students","Group.GroupSubjects.subjectHours.LessonType", "Group.GroupSubjects.Subject", "Group.GroupSubjects.teacherSubjects.TeacherRole", "Group.GroupSubjects.Exams.ExamResults", "Group.GroupSubjects.Exams.ExamType", "Group.GroupSubjects.teacherSubjects.Teacher", "AppUser");
            
            
            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");
            return _mapper.Map<GetStudentForStudentPageDTO>(student);
        }
        public async Task<List<GetStudentForAttendanceForTeacherPageDTO>> GetStudentForAttendanceForTeacherPageAsync(Guid groupSubjectId)
        {
            var students = await _studentRepository.GetFiltered(s=>s.Group.GroupSubjects.Any(gs=>gs.Id == groupSubjectId), "Attendances.SubjectHour.GroupSubject", "Attendances.SubjectHour.LessonType").ToListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentForAttendanceForTeacherPageDTO>>(students);
            return studentsDTO;

        }
        public async Task<List<GetStudentForExamsForTeacherPageDTO>> GetStudentsForExamsForTeacherPageAsync(Guid groupSubjectId)
        {
            List<Student>? students = await _studentRepository.GetFiltered(s => s.Group.GroupSubjects.Any(gs=>gs.Id == groupSubjectId), "examResults").ToListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentForExamsForTeacherPageDTO>>(students);
            return studentsDTO;

        }
        public async Task<List<GetStudentForUser>> GetAllStudentsForUserUpdateAsync()
        {
            var students = await _studentRepository.GetAll().ToListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentForUser>>(students);
            return studentsDTO;
        }
        public async Task<List<GetStudentForCreateOrUpdateForExamResultDTO>> GetStudentsForCreateOrUpdateForExamResultAsync()
        {
            var students= await _studentRepository.GetAll("Group").ToListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentForCreateOrUpdateForExamResultDTO>>(students);
            return studentsDTO;
        }
        public async Task<List<GetStudentForGroupForDetailsDTO>> GetStudentsForGroupForDetailsAsync(Guid groupId)
        {
          List<Student>? students =await   _studentRepository.GetFiltered(s=>s.GroupId == groupId).ToListAsync();
          var studentsDTO =_mapper.Map<List<GetStudentForGroupForDetailsDTO>>(students);
          return studentsDTO;

        }
        public async Task<List<GetStudentForGroupUpdateDTO>> GetStudentsForGoupUpdateAsync(Guid groupId)
        {
            var students = await _studentRepository.GetAll().ToListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentForGroupUpdateDTO>>(students);
            return studentsDTO;
        }
        public async Task CreateStudentAsync(PostStudentDTO postStudentDTO)
        {
            if(postStudentDTO.AppUserId is not null)
            {
                var user = await _userManager.Users.Include(u=>u.Student).FirstOrDefaultAsync(u=>u.Id == postStudentDTO.AppUserId);
                if(user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if(user.Student is not null)
                {
                    throw new UserAlreadyHasStudentException("User is already taken");
                }
                if(user.Teacher is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");
                }
                var roles = await _userManager.GetRolesAsync(user);
                if(roles.Any(r=>r == Roles.Teacher.ToString()))
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");
                }
            }
            //if(postStudentDTO.MainGroup is not null && !(await _groupRepository.IsExistsAsync( g=>g.Id ==  postStudentDTO.MainGroup)))
            //{
            //    throw new GroupNotFoundByIdException($"Main group not found");
            //}
            List<Attendance>? attendances = new List<Attendance>();
            if(postStudentDTO.MainGroup is not null)
            {
                var mainGroup = await _groupRepository.GetSingleAsync(mg=>mg.Id == postStudentDTO.MainGroup, "GroupSubjects");
                if(mainGroup is null)
                {
                    throw new GroupNotFoundByIdException("Group not found");
                }
                mainGroup.StudentCount += 1;
                _groupRepository.Update(mainGroup);
                if (mainGroup.GroupSubjects is not null)
                {
                    var groupSubjects = _groupSubjectRepository.GetFiltered(gs=>gs.GroupId == mainGroup.Id,"subjectHours").ToList();
                    
                    foreach(var groupSubject in groupSubjects)
                    {
                        if(groupSubject.subjectHours is not  null)
                        {
                            foreach(var subjectHour in groupSubject.subjectHours)
                            {
                                var newAttendance = new Attendance()
                                {
                                    SubjectHourId = subjectHour.Id,
                                };
                                attendances.Add(newAttendance);
                            }

                        }
                    }
                }


            }
            var newStudent = _mapper.Map<Student>(postStudentDTO);
            newStudent.Attendances = attendances;
            //if (postStudentDTO.SubGroupsId is not null)
            //{
            //    foreach (var groupId in postStudentDTO.SubGroupsId)
            //    {
            //        if(groupId == postStudentDTO.MainGroup)
            //        {
            //            throw new StudentCannotbeInTwoSameGroupsException("Student cannot be in two same groups");
            //        }

            //    }



            //    List<StudentGroup> newStudentGroups = new List<StudentGroup>();

            //    foreach (var id in postStudentDTO.SubGroupsId)
            //    {
            //        if (!await _groupRepository.IsExistsAsync(g => g.Id == id))
            //            throw new GroupNotFoundByIdException($"Group with Id:{id} not found");

            //        //var studentGroup = new StudentGroup()
            //        //{
            //        //    StudentId = newStudent.Id,
            //        //    GroupId = id,
            //        //};
            //        //newStudentGroups.Add(studentGroup);

            //    }
            //    //newStudent.studentGroups = newStudentGroups;
            //}
            



            if (DateTime.Now.Year - postStudentDTO.DateOfBirth.Year < 18)
            {
                throw new StudentCannotBeYounger("Date of birth must be at least 18 years ago");
            }
             
            await _studentRepository.CreateAsync(newStudent);
            await _studentRepository.SaveChangesAsync();

        }

        public async Task DeleteStudentAsync(Guid Id)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id,"Group");
            if (student is null) 
            {
                throw new StudentNotFoundByIdException("Student not found");
            }
            if(student.Group is not null)
            {
                var group = await _groupRepository.GetSingleAsync(g=>g.Id == student.GroupId);
                group.StudentCount -= 1;
                _groupRepository.Update(group);
            }
            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Guid Id, PutStudentDTO putStudentDTO)
        {
            var student = await _studentRepository.GetSingleAsync(s => s.Id == Id,"AppUser", "studentGroups","Attendances","Group");

            if (student is null)
                throw new StudentNotFoundByIdException("Student not found");

            if (putStudentDTO.AppUserId is not null)
            {
                var user =await  _userManager.Users.Include(u=>u.Student).Include(u => u.Teacher).FirstOrDefaultAsync(u=>u.Id == putStudentDTO.AppUserId);
                if(user is null)
                {
                    throw new UserNotFoundByIdException("User not found");
                }
                if(user.Student is not null  && user.Student.Id != student.Id)
                {
                    throw new UserAlreadyHasStudentException("User is already taken");
                }
                if(user.Teacher is not null)
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");
                }
                var roles =await _userManager.GetRolesAsync(user);
                if (roles.Any(r=>r == Roles.Teacher.ToString()))
                {
                    throw new UserCannotBeStudentAndTeacherException("User  already belongs to the teacher");

                }

            }
            List<Attendance> attendances = new List<Attendance>();
            if(putStudentDTO.groupId is not null && putStudentDTO.groupId != student.GroupId)
            {
                if(student.Group is not null)
                {
                  var oldGroup = await _groupRepository.GetSingleAsync(g => g.Id == student.GroupId);
                    if(oldGroup is null)
                    {
                        throw new GroupNotFoundByIdException("Group Not Found");
                    }
                    oldGroup.StudentCount -= 1;
                    _groupRepository.Update(oldGroup);
                }
                //if (!await _groupRepository.IsExistsAsync(g => g.Id == putStudentDTO.MainGroup))
                var mainGroup = await _groupRepository.GetSingleAsync(g=>g.Id == putStudentDTO.groupId);
                if(mainGroup is null)
                     throw new GroupNotFoundByIdException("Group not found");
                mainGroup.StudentCount += 1;
                _groupRepository.Update(mainGroup);
                var groupSubjects = _groupSubjectRepository.GetFiltered(gs => gs.GroupId == mainGroup.Id, "subjectHours").ToList();
                if(groupSubjects is not null)
                {

                    foreach (var groupSubject in groupSubjects)
                    {
                        if (groupSubject.subjectHours is not null)
                        {
                            foreach (var subjectHour in groupSubject.subjectHours)
                            {
                                var newAttendance = new Attendance()
                                {
                                    SubjectHourId = subjectHour.Id,
                                };
                                attendances.Add(newAttendance);
                            }

                        }
                    }
                }
                


            }
            student = _mapper.Map(putStudentDTO, student);
            student.Attendances?.AddRange(attendances);

            //if (putStudentDTO.GroupId is not null)
            //{
            //    foreach (var groupId in putStudentDTO.GroupId)
            //    {
            //        if (groupId == putStudentDTO.MainGroup)
            //        {
            //            throw new StudentCannotbeInTwoSameGroupsException("Student cannot be in two same groups");
            //        }

            //    }


            //    List<StudentGroup>? groupsToRemove = student.studentGroups?.Where(sg => !putStudentDTO.GroupId.Any(g=>g == sg.GroupId)).ToList();
            //    if(groupsToRemove is not null && groupsToRemove.Count() != 0) 
            //    {
            //        _studentGroupRepository.DeleteList(groupsToRemove);
            //      await  _studentGroupRepository.SaveChangesAsync();
                  
            //    }

            //    List<Guid>? groupsToAdd = putStudentDTO.GroupId.Where(g => !student.studentGroups.Any(sg=>sg.GroupId == g)).ToList();
            //    if(groupsToAdd is not null && groupsToAdd.Count() != 0)
            //    {
            //        List<StudentGroup> newStudentGroups = new List<StudentGroup>();
            //        foreach(var groupId in groupsToAdd)
            //        {
            //            StudentGroup studentGroup = new StudentGroup()
            //            {
            //                GroupId = groupId,
            //                StudentId =student.Id,

            //            };
            //            newStudentGroups.Add(studentGroup);
            //        }

            //        _studentGroupRepository.AddList(newStudentGroups);
            //       await _studentGroupRepository.SaveChangesAsync();
                   



            //    }
            //}



            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();


        }
        public async Task<bool> CheckStudentExistsByIdAsync(Guid id)
        {
          return  await _studentRepository.IsExistsAsync(s=>s.Id == id);
        }

        public async Task<GetStudentForStudentAttendancePageDTO> GetStudentForStudentAttendancePageDTOAsync(Guid studentId ,Guid groupSubjectId)
        {
            var student =await _studentRepository.GetSingleAsync(s => s.Id == studentId && s.Attendances.Any(a => a.SubjectHour.GroupSubjectId == groupSubjectId), "Attendances.SubjectHour.GroupSubject", "Attendances.SubjectHour.LessonType") ;
            if (student is null)
            {
                throw new StudentNotFoundByIdException("Student not found");
            }
            var studentDTO = _mapper.Map<GetStudentForStudentAttendancePageDTO>(student);
            return studentDTO;

        }

       
    }
}
