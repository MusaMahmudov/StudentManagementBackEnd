using FluentValidation;
using StudentManagement.Business.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Validators.StudentValidators
{
    public class PostStudentDTOValidator : AbstractValidator<PostStudentDTO>
    {
        public PostStudentDTOValidator() 
        {
            RuleFor(s => s.FullName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
        }
    }
}
