using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using api.Builders;
using api.Mappers;
using api.Models;
using CsvHelper;
using CsvHelper.Configuration;
using api.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.Interfaces;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace api.Services
{
    public class ImportUsersFromCsv
    {
        private readonly IUserRepository _repository;
        public ImportUsersFromCsv(IUserRepository repository)
        {
            _repository = repository;
        }

        public static ImportUsersFromCsvResponse Import(string filePath)
        {
            var response = new ImportUsersFromCsvResponse();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            List<User> loadedUsers = new List<User>();
            UserBuilder builder = new UserBuilder();
            int line = 0;
            try
            {
                using (var streamReader = new StreamReader(filePath, System.Text.Encoding.UTF8))
                {
                    using (var csvReader = new CsvReader(streamReader, config))
                    {
                        csvReader.Context.RegisterClassMap<UserCsvMap>();

                        var records = csvReader.GetRecords<User>().ToList();


                        foreach (var record in records)
                        {
                            line++;
                            string? name = string.IsNullOrWhiteSpace(record.Name) ? null : record.Name;
                            string surname = record.Surname;  // Předpokládám, že Surname je povinné a nebude null
                            string? adress = string.IsNullOrWhiteSpace(record.Adress) ? null : record.Adress;
                            string? birthNumber = string.IsNullOrWhiteSpace(record.BirthNumber) ? null : record.BirthNumber;
                            string? phone1 = string.IsNullOrWhiteSpace(record.PhoneNumber1) ? null : record.PhoneNumber1;
                            string? phone2 = string.IsNullOrWhiteSpace(record.PhoneNumber2) ? null : record.PhoneNumber2;
                            string? phone3 = string.IsNullOrWhiteSpace(record.PhoneNumber3) ? null : record.PhoneNumber3;



                            User user = builder.WithName(name)
                                                .WithSurname(surname)
                                                .WithBirthNumber(birthNumber)
                                                .WithAdress(adress)
                                                .WithPhoneNumber1(phone1)
                                                .WithPhoneNumber2(phone2)
                                                .WithPhoneNumber3(phone3)
                                                .Build();
                            
                            loadedUsers.Add(user);

                        }
                        response.LoadedUsers = loadedUsers;
                        response.ImportIsComplete = true;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorLine = line;
                response.ImportIsComplete = false;
                response.ResponseText = ex.Message;
                return response;
            }
        }
    }
}