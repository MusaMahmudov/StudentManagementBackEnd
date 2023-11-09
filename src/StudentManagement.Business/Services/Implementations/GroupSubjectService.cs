using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Business.Exceptions.GroupExceptions;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Exceptions.SubjectExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class GroupSubjectService : IGroupSubjectService
    {
        private readonly IGroupSubjectRepository _groupSubjectRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ITeacherSubjectService _teacherSubjectService;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        public GroupSubjectService(IStudentRepository studentRepository,ITeacherSubjectRepository teacherSubjectRepository,ISubjectRepository subjectRepository,IGroupRepository groupRepository,IGroupSubjectRepository groupSubjectRepository,IMapper mapper,AppDbContext context, ITeacherSubjectService teacherSubjectService)
        {
            _studentRepository = studentRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
            _subjectRepository = subjectRepository;
            _groupRepository = groupRepository;
            _teacherSubjectService = teacherSubjectService;
            _context = context;
            _mapper = mapper;
            _groupSubjectRepository = groupSubjectRepository;
        }
        public async Task<List<GetGroupSubjectDTO>> GetAllGroupSubjectsAsync()
        {
          var groupSubjects = await _groupSubjectRepository.GetAll("teacherSubjects.Teacher.teacherSubjects.TeacherRole", "Group.Faculty","Subject").ToListAsync();
          var getGroupSubjects = _mapper.Map<List<GetGroupSubjectDTO>>(groupSubjects);
          return getGroupSubjects;
        }

        public async Task<GetGroupSubjectDTO> GetGroupSubjectByIdAsync(Guid id)
        {
            var groupSubjects = await _groupSubjectRepository.GetSingleAsync(g=>g.Id == id,"teacherSubjects.Teacher.teacherSubjects.TeacherRole", "Group.Faculty", "Subject");
            var getGroupSubjects = _mapper.Map<GetGroupSubjectDTO>(groupSubjects);
            return getGroupSubjects;
        }
        public async Task<GetGroupSubjectForUpdateDTO> GetGroupSubjectForUpdateAsync(Guid id)
        {
           var groupSubject = await _groupSubjectRepository.GetSingleAsync(gs=>gs.Id == id, "teacherSubjects.TeacherRole", "teacherSubjects.Teacher");
            if(groupSubject is null)
            {
                throw new GroupSubjectNotFoundByIdException("Group's Subject not found");
            }
           
            var groupSubjectDTO = _mapper.Map<GetGroupSubjectForUpdateDTO>(groupSubject);
            var teacherRoles = new List<GetTeacherRoleForGroupSubjectForUpdateDTO>();
            if (groupSubject.teacherSubjects?.Count() > 0)
            {
                foreach(var teacherRole in groupSubject.teacherSubjects)
                {
                    var teacherRoleDTO = _mapper.Map<GetTeacherRoleForGroupSubjectForUpdateDTO>(teacherRole);
                    teacherRoles.Add(teacherRoleDTO);
                }
            }
            groupSubjectDTO.teacherRole = teacherRoles;
            return groupSubjectDTO;
        }
        public async Task<List<GetGroupSubjectForSubjectsForStudentPageDTO>> GetGroupSubjectForSubjectsForStudentPageAsync(Guid studentId)
        {
          var student = await _studentRepository.GetSingleAsync(s => s.Id == studentId,"Group.GroupSubjects.Subject", "Group.GroupSubjects.teacherSubjects.TeacherRole", "Group.GroupSubjects.teacherSubjects.Teacher", /*"Group.GroupSubjects.subjectHours"*/ "Group.GroupSubjects.Exams", "studentGroups");
            if(student is null)
            {
                throw new StudentNotFoundByIdException("Student not found");
            }
            List<GroupSubject> groupSubjects = new List<GroupSubject>();
          
            if(student.Group is not null)
            {
                if(student.Group.GroupSubjects.Count() > 0)
                {
                    foreach(var groupSubject in student.Group.GroupSubjects)
                    {
                        groupSubjects.Add(groupSubject);
                    }
                }

            }
            if(student.studentGroups.Count() > 0)
            {
                foreach(var studentGroup in  student.studentGroups)
                {
                    if(studentGroup.Group.GroupSubjects.Count() > 0)
                    {
                        foreach(var groupSubject in studentGroup.Group.GroupSubjects)
                        {
                            groupSubjects.Add(groupSubject);
                        }
                    }
                }
            }
            var groupSubjectsDTO = _mapper.Map<List<GetGroupSubjectForSubjectsForStudentPageDTO>>(groupSubjects);
            return groupSubjectsDTO;

        }
        public async Task CreateGroupSubjectAsync(PostGroupSubjectDTO postGroupSubjectDTO)
        {
            var newGroupSubject = _mapper.Map<GroupSubject>(postGroupSubjectDTO);
            if (!await _groupRepository.IsExistsAsync(g=> g.Id == postGroupSubjectDTO.GroupId))
            {
                throw new GroupNotFoundByIdException("Group not found");
            }
            if(!await _subjectRepository.IsExistsAsync(s=>s.Id == postGroupSubjectDTO.SubjectId))
            {
                throw new SubjectNotFoundByIdException("Subject not found");
            }
            if(await  _groupSubjectRepository.IsExistsAsync(gs=>gs.GroupId == postGroupSubjectDTO.GroupId && gs.SubjectId == postGroupSubjectDTO.SubjectId))
            {
                throw new GroupSubjectAlreadyExistsException("Group with this subject already exists");
            }

            



            await _groupSubjectRepository.CreateAsync(newGroupSubject);
            await _groupSubjectRepository.SaveChangesAsync();


            if(postGroupSubjectDTO.teacherRole is not null)
            {
                List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
                foreach(var teacherRole in postGroupSubjectDTO.teacherRole)
                {
                    var teacherSubject = new TeacherSubject()
                    {
                        TeacherId = teacherRole.TeacherId,
                        GroupSubjectId = newGroupSubject.SubjectId,
                        TeacherRoleId = teacherRole.RoleId
                        
                     };
                    teacherSubjects.Add(teacherSubject);
                }
                //await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);
                _teacherSubjectRepository.AddList(teacherSubjects);
                newGroupSubject.teacherSubjects = teacherSubjects;
                await _teacherSubjectRepository.SaveChangesAsync();

            }


        }

        public async Task DeleteGroupSubjectAsync(Guid id)
        {
            var existingGroupSubject = await _groupSubjectRepository.GetSingleAsync(gs=>gs.Id == id);
            if (existingGroupSubject is null)
                throw new GroupSubjectNotFoundByIdException("Group's subject not found");

            if (existingGroupSubject.teacherSubjects is not null)
            {
                var existingTeacherSubjects = await _teacherSubjectService.GetTeacherSubjectsForGroupSubjectAsync(existingGroupSubject.Id);
               await _teacherSubjectService.DeleteTeacherSubjectsAsync(existingTeacherSubjects);
            }



            _groupSubjectRepository.Delete(existingGroupSubject);
          await _groupSubjectRepository.SaveChangesAsync();
        }

       

        public async Task UpdateGroupSubjectAsync(Guid id, PutGroupSubjectDTO putGroupSubjectDTO)
        {
            var existingGroupSubject = await _groupSubjectRepository.GetSingleAsync(gs => gs.Id == id, "teacherSubjects");
            
            if (existingGroupSubject is null)
                throw new GroupSubjectNotFoundByIdException("Group's subject not found");
            if(putGroupSubjectDTO.GroupId !=existingGroupSubject.GroupId || putGroupSubjectDTO.SubjectId !=existingGroupSubject.SubjectId) 
            {
                if (await _groupSubjectRepository.IsExistsAsync(gs => gs.GroupId == putGroupSubjectDTO.GroupId && gs.SubjectId == putGroupSubjectDTO.SubjectId))
                {
                    throw new GroupSubjectAlreadyExistsException("Group with this subject already exists");
                }
            }
           
            existingGroupSubject = _mapper.Map(putGroupSubjectDTO,existingGroupSubject);
            _groupSubjectRepository.Update(existingGroupSubject);
            await _groupSubjectRepository.SaveChangesAsync();


            if (putGroupSubjectDTO.teacherRole is not null)
            {


                List<TeacherSubject>? teachersToRemove = existingGroupSubject.teacherSubjects?.Where(ts => !putGroupSubjectDTO.teacherRole.Any(tr => tr.TeacherId == ts.TeacherId && tr.RoleId == ts.TeacherRoleId)).ToList();
                var teachersRoleAdd = putGroupSubjectDTO.teacherRole.Where(tr => !existingGroupSubject.teacherSubjects.Any(ts => ts.TeacherId == tr.TeacherId && ts.TeacherRoleId == tr.RoleId)).ToList();

                if (teachersToRemove is not null)
                {
                    await _teacherSubjectService.DeleteTeacherSubjectsAsync(teachersToRemove);
                }

                List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
                foreach (var teacherRole in teachersRoleAdd)
                {
                    var teacherSubject = new TeacherSubject()
                    {
                        TeacherId = teacherRole.TeacherId,
                        GroupSubjectId = existingGroupSubject.SubjectId,
                        TeacherRoleId = teacherRole.RoleId

                    };
                    teacherSubjects.Add(teacherSubject);
                }
                await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);
                existingGroupSubject.teacherSubjects?.AddRange(teacherSubjects);
                await _context.SaveChangesAsync();

            }
            else
            {
                if(existingGroupSubject.teacherSubjects != null)
                {
                    List<TeacherSubject> teachersToRemove = existingGroupSubject.teacherSubjects.ToList();
                    await _teacherSubjectService.DeleteTeacherSubjectsAsync(teachersToRemove);
                    existingGroupSubject.teacherSubjects = null;
                    await _context.SaveChangesAsync();

                }
            }
        }

        public List<GetGroupSubjectForTeacherPageDTO> GetGroupSubjectForTeacherPageDTO(Guid teacherId)
        {
           var groupSubject = _groupSubjectRepository.GetFiltered(gs=>gs.teacherSubjects.Any(ts=>ts.TeacherId == teacherId),"Exams.ExamType", "Exams.ExamResults", "Group","Subject", "teacherSubjects.TeacherRole", "teacherSubjects.Teacher","Exams").ToList();
            var groupSubjectDTO = _mapper.Map<List<GetGroupSubjectForTeacherPageDTO>>(groupSubject);
            return groupSubjectDTO;

        }

        public async Task<List<GetGroupSubjectForExamUpdateDTO>> GetGroupSubjectsForExamUpdateAsync()
        {
            var groupSubjects = await _groupSubjectRepository.GetAll("Group","Subject").ToListAsync();
            var groupSubjectsDTO = _mapper.Map<List<GetGroupSubjectForExamUpdateDTO>>(groupSubjects);
            return groupSubjectsDTO;
        }
    }
}
