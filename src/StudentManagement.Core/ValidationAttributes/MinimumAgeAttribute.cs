using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.ValidationAttributes
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        public MinimumAgeAttribute(int minimumAge) 
        { 
         _minimumAge = minimumAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime DateOfBirth)
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                if(age < _minimumAge) 
                {
                    return new ValidationResult($"Date of birth must be at least {_minimumAge} years ago.");
                }
                
            }

            return ValidationResult.Success;
        }

    }
}
