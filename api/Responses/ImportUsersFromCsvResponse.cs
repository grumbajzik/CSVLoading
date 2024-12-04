using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Responses
{
    public class ImportUsersFromCsvResponse
    {
        public List<User> LoadedUsers{get;set;} = new List<User>();

        public bool ImportIsComplete {get;set;}

        public int ErrorLine{get;set;}
        public int ModifiedLines {get;set;} = 0;

        public int CreatedLines {get;set;} = 0;

        public string ResponseText {get;set;} = string.Empty;
        

    }
}