﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class PutGroupDTO
    {
        public string Name { get; set; }
        public byte Year { get; set; }
        public Guid FacultyId { get; set; }
        public List<Guid>? StudentsId { get; set; }
    }
}
