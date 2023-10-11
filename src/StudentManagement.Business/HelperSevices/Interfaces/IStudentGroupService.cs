using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Interfaces
{
    public interface IStudentGroupService
    {
        Task DeleteStudentGroupList(List<StudentGroup> studentGroups);
    }
}
