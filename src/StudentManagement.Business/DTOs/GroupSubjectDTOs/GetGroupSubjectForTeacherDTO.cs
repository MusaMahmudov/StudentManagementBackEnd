﻿using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForTeacherDTO
    {
        public GetGroupForTeacherDTO Group { get; set; }
        public string SubjectName { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
    }
}