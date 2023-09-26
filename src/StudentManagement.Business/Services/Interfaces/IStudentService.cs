﻿using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<List<GetStudentDTO>> GetAllStudentsAsync(string? search);
        public Task<GetStudentDTO> GetStudentByIdAsync(Guid Id);

        public Task CreateStudentAsync(PostStudentDTO postStudentDTO);
        public Task DeleteStudentAsync(Guid Id);
        public Task UpdateStudentAsync(Guid Id,PostStudentDTO postStudentDTO);

    }
}
