using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Responses;

namespace api.Interfaces
{
    public interface IUserRepository {
        Task<List<User>> FindAllAsync();

        Task<User?> FindById(int id);

        Task<User?> FindByBirthNumber(string birthNumber);

        Task<User> Save(User user);

        Task<User?> UpdateById(int id, UserRequestDto userDto);

        User? UpdateByBirthNumber(string birthNumber, UserUpdateByBirthNumberDto userDto, ApplicationDBContext context);

        Task<User?> DeleteById(int id);

        Task<ImportUsersFromCsvResponse> importUsersFromCsv(string filePath);
    }
}