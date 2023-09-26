using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities
{
    public class Faculty : BaseSectionEntity
    {
        public string Name { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}
