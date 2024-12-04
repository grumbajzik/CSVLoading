using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using CsvHelper.Configuration.Attributes;

namespace api.Validators
{
     public class UniqueBirthNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDBContext)validationContext.GetService(typeof(ApplicationDBContext));
            var birthNumber = value as string;

            // Ověření, že rodné číslo je číselné
            if (!string.IsNullOrEmpty(birthNumber) && !birthNumber.All(char.IsDigit))
            {
                return new ValidationResult("BirthNumber must be numeric.");
            }

            // Ověření unikátnosti
            if (context.Users.Any(u => u.BirthNumber == birthNumber))
            {
                return new ValidationResult("BirthNumber must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
