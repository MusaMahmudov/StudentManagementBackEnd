﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForGroupForStudentPageDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}