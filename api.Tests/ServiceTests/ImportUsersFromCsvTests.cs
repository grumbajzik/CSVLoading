using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;  // Používáme xUnit pro testy
using api.Services;
//using api.Responses;



{
    
}

namespace api.Tests.ServiceTests
{
    public class ImportUsersFromCsvTests
    {
        [Fact]
        public void ImportTest()
        {
            // Given
            string filePath = "data.csv";
            var data = new List<string[]>
        {
            new string[] { "Jméno", "Příjmení", "RČ", "Adresa", "Telefon 1", "Telefon 2", "Telefon 3" },
            new string[] { "Jan", "Novák", "8001011234", "Praha, Ulice 1, 10000", "123456789", "987654321" },
            new string[] { "Petr", "Horák", "", "Brno, Ulice 2, 20000", "", "111222333"},
            new string[] { "Lucie", "Svobodová", "9102034567", "", "", "", "" }
        };
        
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var line in data)
            {
                writer.WriteLine(string.Join(";", line));  // Spojí hodnoty do jednoho řádku s čárkami
            }
        }

        var response = ImportUsersFromCsv.Import(filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("\nCSV soubor byl smazán.");
            }
            Assert.Equal(3, response.LoadedUsers.Count);

        }
    }
}