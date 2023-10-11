using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.LessonTypeDTOs;
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
    public class LessonTypeService : ILessonTypeService
    {
        private readonly ILessonTypeRepository _lessonTypeRepository;
        private readonly IMapper _mapper;
        public LessonTypeService(ILessonTypeRepository lessonTypeRepository,IMapper mapper)
        {
            _mapper = mapper;
            _lessonTypeRepository = lessonTypeRepository;
        }
        public async Task<List<GetLessonTypeDTO>> GetAllLessonTypesAsync(string? search)
        {
            var lessonTypes = await _lessonTypeRepository.GetFiltered(lt=> search != null ? lt.Name.Contains(search) : true).ToListAsync();
            var lessonTypesDTO = _mapper.Map<List<GetLessonTypeDTO>>(lessonTypes);
            return lessonTypesDTO;
        }

        public async Task<GetLessonTypeDTO> GetLessonTypeByIdAsync(Guid Id)
        {
            var lessonType = await _lessonTypeRepository.GetSingleAsync(lt=>lt.Id == Id);
            if (lessonType is null)
                throw new LessonTypeNotFoundByIdException("Lesson's type not found");

            GetLessonTypeDTO lessonTypeDTO = _mapper.Map<GetLessonTypeDTO>(lessonType);
            return lessonTypeDTO;

        }


        public async Task CreateLessonTypeAsync(PostLessonTypeDTO postLessonTypeDTO)
        {
            var newLessonType = _mapper.Map<LessonType>(postLessonTypeDTO);
            await _lessonTypeRepository.CreateAsync(newLessonType);
           await _lessonTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteLessonTypeAsync(Guid Id)
        {
            var lessonType = await _lessonTypeRepository.GetSingleAsync(lt => lt.Id == Id);
            if (lessonType is null)
                throw new LessonTypeNotFoundByIdException("Lesson's type not found");

            _lessonTypeRepository.Delete(lessonType);
           await _lessonTypeRepository.SaveChangesAsync();
        }

        
        public async Task UpdateLessonTypeAsync(Guid Id, PutLessonTypeDTO putLessonTypeDTO)
        {
            var lessonType = await _lessonTypeRepository.GetSingleAsync(lt => lt.Id == Id);
            if (lessonType is null)
                throw new LessonTypeNotFoundByIdException("Lesson's type not found");

            lessonType = _mapper.Map(putLessonTypeDTO,lessonType);
            _lessonTypeRepository.Update(lessonType);
            await _lessonTypeRepository.SaveChangesAsync();
        }
    }
}
