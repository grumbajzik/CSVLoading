using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;
using CsvHelper.Configuration;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static User ToUserFromRequestDto(this UserRequestDto model){
            return new User {
                Name = model.Name,
                Surname = model.Surname,
                BirthNumber = model.BirthNumber,
                Adress = model.Adress,
                PhoneNumber1 = model.PhoneNumber1,
                PhoneNumber2 = model.PhoneNumber2,
                PhoneNumber3 = model.PhoneNumber3
            };
        }

        public static UserUpdateByBirthNumberDto ToUserUpdateByBirthNameDtoFromUser(this User model) {
            return new UserUpdateByBirthNumberDto {
                Name = model.Name,
                Surname = model.Surname,
                Adress = model.Adress,
                PhoneNumber1 = model.PhoneNumber1,
                PhoneNumber2 = model.PhoneNumber2,
                PhoneNumber3 = model.PhoneNumber3
            };
        }
    }
}