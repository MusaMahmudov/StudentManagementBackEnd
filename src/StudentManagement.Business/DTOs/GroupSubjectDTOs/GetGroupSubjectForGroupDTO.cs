﻿using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForGroupDTO
    {
        public Guid Id { get; set; }
        public GetSubjectDTO Subject { get; set; }
        public List<GetTeacherForGroupDTO>? Teachers { get; set; }
        public byte Credits { get; set; }
        public byte Hours { get; set; }
        public byte TotalWeeks { get; set; }
    }
}
