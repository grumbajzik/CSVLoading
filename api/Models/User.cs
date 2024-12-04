using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Validators;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;


namespace api.Models
{
    [UniqueBirthNumber]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required(ErrorMessage = "Surname is Requiered")]
        public required string Surname { get; set; }

        public string? Adress { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "BirthNumber must be numeric and exactly 10 digits.")]

        public string? BirthNumber { get; set; }

        [StringLength(9, MinimumLength = 9)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be numeric and exactly 9 digits.")]
        public string? PhoneNumber1 { get; set; }
        [StringLength(9, MinimumLength = 9)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be numeric and exactly 9 digits.")]
        public string? PhoneNumber2 { get; set; }
        [StringLength(9, MinimumLength = 9)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be numeric and exactly 9 digits.")]
        public string? PhoneNumber3 { get; set; }
    }
}