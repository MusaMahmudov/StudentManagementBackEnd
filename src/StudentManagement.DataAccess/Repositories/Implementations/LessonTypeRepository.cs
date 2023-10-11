﻿using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Repositories.Implementations
{
    public class LessonTypeRepository : Repository<LessonType>, ILessonTypeRepository
    {
        public LessonTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
