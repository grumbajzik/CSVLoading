using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Responses;
using api.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<User>> FindAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindByBirthNumber(string birthNumber)
        {
            if (birthNumber == null)
            {
                return null;
            }
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.BirthNumber == birthNumber);
            if (userModel == null)
            {
                return null;
            }
            return userModel;

        }

        public async Task<User?> FindById(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }

        public async Task<User> Save(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateById(int id, UserRequestDto userDto)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return null;
            }
            userModel.Name = userDto.Name;
            userModel.Surname = userDto.Surname;
            userModel.BirthNumber = userDto.BirthNumber;
            userModel.Adress = userDto.Adress;
            userModel.PhoneNumber1 = userDto.PhoneNumber1;
            userModel.PhoneNumber2 = userDto.PhoneNumber2;
            userModel.PhoneNumber3 = userDto.PhoneNumber3;

            await _context.SaveChangesAsync();
            return userModel;
        }

        public User UpdateByBirthNumber(string birthNumber, UserUpdateByBirthNumberDto userDto, ApplicationDBContext context)
        {
            var userModel = context.Users.FirstOrDefault(x => x.BirthNumber == birthNumber);
            if (userModel == null) return userModel;

            userModel.Name = userDto.Name;
            userModel.Surname = userDto.Surname;
            userModel.Adress = userDto.Adress;
            userModel.PhoneNumber1 = userDto.PhoneNumber1;
            userModel.PhoneNumber2 = userDto.PhoneNumber2;
            userModel.PhoneNumber3 = userDto.PhoneNumber3;

            // Nepoužívejte zde context.SaveChanges!
            return userModel;
        }

        public async Task<User?> DeleteById(int id)
        {
            var userModel = await FindById(id);
            if (userModel == null) return null;

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<ImportUsersFromCsvResponse> importUsersFromCsv(string filePath)
        {
            var response = ImportUsersFromCsv.Import(filePath);

            if (!response.ImportIsComplete) return response;


            try
            {
                // Přidání importovaných uživatelů do databáze
                var usersToAdd = new List<User>();
                var usersToUpdate = new List<User>();
                foreach (var user in response.LoadedUsers)
                {
                    var existingUser = await FindByBirthNumber(user.BirthNumber);
                    if (existingUser == null)
                    {
                        usersToAdd.Add(user);
                    }
                    else
                    {
                        usersToUpdate.Add(user);
                    }

                }

                await _context.AddRangeAsync(usersToAdd);
                foreach (var user in usersToUpdate)
                {
                    var userModel = user.ToUserUpdateByBirthNameDtoFromUser();
                    UpdateByBirthNumber(user.BirthNumber, userModel, _context);
                }
                response.ModifiedLines = _context.ChangeTracker.Entries()
                                        .Where(e => e.State == EntityState.Modified)
                                        .Count(); 
                await _context.SaveChangesAsync();

                // Počet přidaných záznamů
                response.CreatedLines = usersToAdd.Count();

                                       
            }
            catch (Exception ex)
            {
                // Ošetření chyb během ukládání
                response.ImportIsComplete = false;
                response.ResponseText = $"Chyba při ukládání do databáze: {ex.Message}";
            }

            // Vrácení odpovědi s detaily o procesu
            return response;
        }
    }
}