using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.Exceptions.GroupSubjectExceptions;
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

        public GroupSubjectService(IGroupSubjectRepository groupSubjectRepository,IMapper mapper,AppDbContext context, ITeacherSubjectService teacherSubjectService)
        {
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
        public async Task CreateGroupSubjectAsync(PostGroupSubjectDTO postGroupSubjectDTO)
        {
            var newGroupSubject = _mapper.Map<GroupSubject>(postGroupSubjectDTO);
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
                await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);
                newGroupSubject.teacherSubjects = teacherSubjects;
                await _context.SaveChangesAsync();

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
    }
}
