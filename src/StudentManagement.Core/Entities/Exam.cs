using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class Exam : BaseSectionEntity
    {
        public string Name {  get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public ExamType ExamType { get; set; }
        public Guid ExamTypeId { get; set; }
        public GroupSubject GroupSubject { get; set; }
        public Guid GroupSubjectId { get; set; }
        public List<ExamResult>? ExamResults { get; set; }
        public byte MaxScore { get; set; }

    }
}
