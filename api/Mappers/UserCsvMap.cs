using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using CsvHelper.Configuration;

namespace api.Mappers
{
   public class UserCsvMap : ClassMap<Models.User>
{
    public UserCsvMap() {
        Map(m => m.Name).Name("Jméno").Optional();
        Map(m => m.Surname).Name("Příjmení");
        Map(m => m.BirthNumber).Name("RČ").Optional();
        Map(m => m.Adress).Name("Adresa").Optional();
        Map(m => m.PhoneNumber1).Name("Telefon 1").Optional(); 
        Map(m => m.PhoneNumber2).Name("Telefon 2").Optional();
        Map(m => m.PhoneNumber3).Name("Telefon 3").Optional();
    }
}
}