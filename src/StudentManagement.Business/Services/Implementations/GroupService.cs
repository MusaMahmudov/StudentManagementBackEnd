using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.Exceptions.GroupExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Enums;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IFacultyRepository _faultyRepository;

        public GroupService(IFacultyRepository facultyRepository,IMapper mapper, IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            _faultyRepository = facultyRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }
        public async Task<List<GetGroupDTO>> GetAllGroupsAsync(string? search)
        {
            var Groups = await _groupRepository.GetFiltered(g=> search != null ? g.Name.Contains(search) : true,"Faculty", "studentGroups.Student", "GroupSubjects.Subject", "GroupSubjects.teacherSubjects.Teacher", "GroupSubjects.teacherSubjects.TeacherRole").ToListAsync();
            var getGroupDTO = _mapper.Map<List<GetGroupDTO>>(Groups);
            return getGroupDTO;
        }

        public async Task<GetGroupDTO> GetGroupByIdAsync(Guid id)
        {
            var Group = await _groupRepository.GetSingleAsync(f=>f.Id == id,"Students","Faculty","studentGroups.Student", "GroupSubjects.Subject", "GroupSubjects.teacherSubjects.Teacher", "GroupSubjects.teacherSubjects.TeacherRole");
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");

            
            var getGroupDTO = _mapper.Map<GetGroupDTO>(Group);
            return getGroupDTO;

        }
        public async Task<GetGroupForUpdateDTO> GetGroupByIdForUpdateAsync(Guid id)
        {
            var Group = await _groupRepository.GetSingleAsync(f => f.Id == id, "Students","Faculty");
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");


            var getGroupDTO = _mapper.Map<GetGroupForUpdateDTO>(Group);
            return getGroupDTO;
        }
        public async Task CreateGroupAsync(PostGroupDTO postGroupDTO)
        {
            var students =new List<Student>();  
            if (postGroupDTO.StudentsId is not null || postGroupDTO.StudentsId?.Count() > 0)
            {

                foreach (var studentId in postGroupDTO.StudentsId)
                {
                    var student = await _studentRepository.GetSingleAsync(s => s.Id == studentId, "Group.Faculty");
                    if (student is null)
                        throw new StudentNotFoundByIdException("Student not found");
                    if (student.Group is not null)
                    {
                        throw new StudentAlreadyHasMainGroup($"Student: {student.FullName} already has group");
                    }
                    students.Add(student);

                }


                postGroupDTO.StudentCount = (byte)postGroupDTO.StudentsId.Count();
            }
            else
            {
                postGroupDTO.StudentCount = 0;
            }
            if(!await _faultyRepository.IsExistsAsync(f=>f.Id == postGroupDTO.FacultyId))
            {
                throw new GroupNotFoundByIdException("Faculty not found");
            }


            var newGroup = _mapper.Map<Group>(postGroupDTO);
            newGroup.Students = students;
            if(newGroup.Students is not null)
            {
                newGroup.StudentCount = (byte)newGroup.Students.Count();

            }
            //if(newGroup.studentGroups is not null || newGroup.studentGroups?.Count() > 0)
            //{
            //    foreach(var studentInGroup in newGroup.studentGroups)
            //    {
            //        var existingStudent =await _studentRepository.GetSingleAsync(s => s.Id == studentInGroup.StudentId);

            //        if(existingStudent is null)
            //            throw new StudentNotFoundByIdException("Student not found");

            //        studentInGroup.Student = existingStudent;

            //    }
            //}


            await _groupRepository.CreateAsync(newGroup);
            await _groupRepository.SaveChangesAsync();


        }

        public async Task DeleteGroupAsync(Guid id)
        {
            var Group = await _groupRepository.GetSingleAsync(g=>g.Id == id);
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");
            _groupRepository.Delete(Group);
            await _groupRepository.SaveChangesAsync();
        }



        public async Task UpdateGroupAsync(Guid Id, PutGroupDTO putGroupDTO)
        {
            var Group = await _groupRepository.GetSingleAsync(g => g.Id == Id,"Students");
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");
           var students =new List<Student>();
            if(putGroupDTO.StudentsId.Count() > 0)
            {
                foreach(var studentId in putGroupDTO.StudentsId)
                {
                    var student = await _studentRepository.GetSingleAsync(s => s.Id == studentId,"Group");
                    if(student is null)
                    {
                        throw new StudentNotFoundByIdException("Student not found");
                    }
                    if(student.Group is not null && student.GroupId != Id)
                    {
                        throw new StudentAlreadyHasMainGroup($"Student : {student.FullName}  already in group");
                    }
                    students.Add(student);
                }
            }
            Group.Students = students;
            if(putGroupDTO.StudentsId.Count() == 0)
            {
                foreach (var studentId in Group.Students.Select(s => s.Id))
                {
                    var student = await _studentRepository.GetSingleAsync(s => s.Id == studentId);
                    if (student is null)
                    {
                        throw new StudentNotFoundByIdException("Student not found");
                    }
                    student.Group = null;
                    _studentRepository.Update(student);
                }
            }
           
            
            Group = _mapper.Map(putGroupDTO, Group);
            
            if (Group.Students is not null && Group.Students.Count() > 0)
            {
                Group.StudentCount = (byte)putGroupDTO.StudentsId?.Count();
            }
            else
            {
                Group.StudentCount = 0;

            }
            
            _groupRepository.Update(Group);
           await _groupRepository.SaveChangesAsync();
        }

        public async Task<List<GetGroupForObjectsUpdateDTO>> GetGroupsForObjectsUpdateAsync()
        {
            var groups = await _groupRepository.GetAll().ToListAsync();
            var groupsDTO = _mapper.Map<List<GetGroupForObjectsUpdateDTO>>(groups);
            return groupsDTO;
        }


        //public async Task AddStudentAsync(Guid Id) 
        //{
        //    var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);
        //    if (student is null)
        //        throw new StudentNotFoundByIdException("Student not found");


        //}
    }
}
