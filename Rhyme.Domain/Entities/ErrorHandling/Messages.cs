using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyme.Domain.Entities.ErrorHandling
{
    public class Messages
    {
        public string UserAlreadyExist = "User Already Exist_502";
        public string Login = "Please Check Creds_502";
        public string kafkaError = "Problem Processing Data_502";
        public string eleNotFound = "No Element Found_409";
        public string AppidRange = "App id is not in a range_409";
    }
}