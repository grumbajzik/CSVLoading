using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using api.Models;

namespace api.Builders
{
    public class UserBuilder
    {
        private string? _name;
        private string _surname = string.Empty; // Povinné pole
        private string? _adress;
        private string? _birthNumber;
        private string? _phoneNumber1;
        private string? _phoneNumber2;
        private string? _phoneNumber3;

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentException("Surname is required and cannot be null or empty.", nameof(surname));
            }

            _surname = surname;
            return this;
        }

        public UserBuilder WithAdress(string adress)
        {
            _adress = adress;
            return this;
        }

        public UserBuilder WithBirthNumber(string? birthNumber)
        {
            if (birthNumber == null)
            {
                return this;
            }
            if (!Regex.IsMatch(birthNumber, @"^\d{10}$")&& birthNumber!= null)
            {
                throw new ArgumentException("Birth number must "+ birthNumber +" be exactly 10 digits.", nameof(birthNumber));
            }

            _birthNumber = birthNumber;
            return this;
        }

        public UserBuilder WithPhoneNumber1(string phoneNumber)
        {
            if(phoneNumber == null) {
                return this;
            }
            ValidatePhoneNumber(phoneNumber, nameof(phoneNumber));
            _phoneNumber1 = phoneNumber;
            return this;
        }

        public UserBuilder WithPhoneNumber2(string phoneNumber)
        {
            if(phoneNumber == null) {
                return this;
            }
            ValidatePhoneNumber(phoneNumber, nameof(phoneNumber));
            _phoneNumber2 = phoneNumber;
            return this;
        }

        public UserBuilder WithPhoneNumber3(string phoneNumber)
        {
            if(phoneNumber == null) {
                return this;
            }
            ValidatePhoneNumber(phoneNumber, nameof(phoneNumber));
            _phoneNumber3 = phoneNumber;
            return this;
        }

        public User Build()
        {
            if (string.IsNullOrWhiteSpace(_surname))
            {
                throw new InvalidOperationException("Cannot build User: Surname is required.");
            }

            return new User
            {
                Name = _name,
                Surname = _surname,
                BirthNumber = _birthNumber,
                Adress = _adress,
                PhoneNumber1 = _phoneNumber1,
                PhoneNumber2 = _phoneNumber2,
                PhoneNumber3 = _phoneNumber3
            };
        }

        private void ValidatePhoneNumber(string phoneNumber, string parameterName)
        {
            if (!Regex.IsMatch(phoneNumber, @"^\d{9}$"))
            {
                throw new ArgumentException("Phone number:" + phoneNumber + " must be exactly 9 digits.", parameterName);
            }
        }
    }
}