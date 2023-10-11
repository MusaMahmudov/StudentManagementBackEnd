using StudentManagement.Business.HelperSevices.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Implementations
{
    public class StudentGroupService : IStudentGroupService
    {
        private readonly IStudentGroupRepository _studentGroupRepository;
        public StudentGroupService(IStudentGroupRepository studentGroupRepository)
        {
            _studentGroupRepository = studentGroupRepository;
        }
        public async Task DeleteStudentGroupList(List<StudentGroup> studentGroups)
        {
            _studentGroupRepository.DeleteList(studentGroups);
           await _studentGroupRepository.SaveChangesAsync();
        }
    }
}
