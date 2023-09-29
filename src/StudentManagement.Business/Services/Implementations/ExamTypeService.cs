﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.ExamTypeDTOs;
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
    public class ExamTypeService : IExamTypeService
    {
        private readonly IExamTypeRepository _examTypeRepository;
        private readonly IMapper _mapper;
        public ExamTypeService(IExamTypeRepository examTypeRepository,IMapper mapper)
        {
            _mapper = mapper;
            _examTypeRepository = examTypeRepository;
        }

        public async Task<List<GetExamTypeDTO>> GetAllExamTypesAsync(string? search)
        {
            var examTypes = await _examTypeRepository.GetFiltered(e=> search!= null ? e.Name.Contains(search) : true).ToListAsync();
            var examTypesDTO = _mapper.Map<List<GetExamTypeDTO>>(examTypes);
            return examTypesDTO;



        }

        public async Task<GetExamTypeDTO> GetExamTypeByIdAsync(Guid id)
        {
            var examType = await _examTypeRepository.GetSingleAsync(e => e.Id == id);
            var examTypeDTO = _mapper.Map<GetExamTypeDTO>(examType);
            return examTypeDTO;
        }
        public async Task CreateExamTypeAsync(PostExamTypeDTO postExamTypeDTO)
        {

            var newExamType = _mapper.Map<ExamType>(postExamTypeDTO);
           await _examTypeRepository.CreateAsync(newExamType);
           await _examTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteExamTypeAsync(Guid id)
        {
            var examType = await _examTypeRepository.GetSingleAsync(e => e.Id == id);
            _examTypeRepository.Delete(examType);
            _examTypeRepository.Update(examType);
        }

       

        public async Task UpdateExamTypeAsync(Guid id, PostExamTypeDTO postExamTypeDTO)
        {
            var examType = await _examTypeRepository.GetSingleAsync(e=>e.Id == id);
            examType = _mapper.Map(postExamTypeDTO,examType);
            _examTypeRepository.Update(examType);
           await _examTypeRepository.SaveChangesAsync();


        }
    }
}
