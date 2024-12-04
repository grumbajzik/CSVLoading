using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class UserUpdateByBirthNumberDto
    {
       
    public string? Name { get; set; }

    [Required(ErrorMessage = "Surname is Required")]
    public required string Surname { get; set; }

    public string? Adress { get; set; }

    [StringLength(10, MinimumLength = 10)]
    public string? BirthNumber { get; set; }

    [StringLength(9, MinimumLength = 9)]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
    public string? PhoneNumber1 { get; set; }

    [StringLength(9, MinimumLength = 9)]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
    public string? PhoneNumber2 { get; set; }

    [StringLength(9, MinimumLength = 9)]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
    public string? PhoneNumber3 { get; set; }
    }
}