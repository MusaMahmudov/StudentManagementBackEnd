using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Repositories.Implementations
{
    internal class ExamTypeRepository : Repository<ExamType>, IExamTypeRepository
    {
        public ExamTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
