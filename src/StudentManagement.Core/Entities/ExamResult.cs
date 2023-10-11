using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class ExamResult : BaseSectionEntity
    {
        public Student Student { get; set; }
        public Guid StudentId { get; set; }
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
        public byte? Score { get; set; }
    }
}
