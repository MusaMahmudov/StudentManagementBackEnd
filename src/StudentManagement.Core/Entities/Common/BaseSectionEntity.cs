using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities.Common;

public abstract class BaseSectionEntity : BaseEntity
{
    public DateTime CreatedAt { get; set;}
    public string CreatedBy { get; set;}
    public DateTime UpdatedAt { get; set;}
    public string UpdatedBy { get; set;}

}
