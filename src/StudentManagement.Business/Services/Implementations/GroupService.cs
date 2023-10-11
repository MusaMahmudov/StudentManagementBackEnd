﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.Exceptions.GroupExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
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
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public GroupService(IMapper mapper, IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
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
            var Group = await _groupRepository.GetSingleAsync(f=>f.Id == id,"Faculty","studentGroups.Student", "GroupSubjects.Subject", "GroupSubjects.teacherSubjects.Teacher", "GroupSubjects.teacherSubjects.TeacherRole");
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");

            
            var getGroupDTO = _mapper.Map<GetGroupDTO>(Group);
            return getGroupDTO;

        }
        public async Task CreateGroupAsync(PostGroupDTO postGroupDTO)
        {
            var newGroup = _mapper.Map<Group>(postGroupDTO);
            if(newGroup.studentGroups is not null)
            {
                newGroup.StudentCount = (byte)postGroupDTO.StudentsId.Count();
            }
            else
            {
                newGroup.StudentCount = 0;
            }


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



        public async Task UpdateGroupAsync(Guid Id, PostGroupDTO postGroupDTO)
        {
            var Group = await _groupRepository.GetSingleAsync(g => g.Id == Id);
            if (Group is null)
                throw new GroupNotFoundByIdException("Group not found");
           

            
            Group = _mapper.Map(postGroupDTO, Group);
            Group.StudentCount = (byte)postGroupDTO.StudentsId.Count();
            _groupRepository.Update(Group);
           await _groupRepository.SaveChangesAsync();
        }
        //public async Task AddStudentAsync(Guid Id) 
        //{
        //    var student = await _studentRepository.GetSingleAsync(s => s.Id == Id);
        //    if (student is null)
        //        throw new StudentNotFoundByIdException("Student not found");


        //}
    }
}
