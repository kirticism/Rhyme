using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyme.Domain
{
    public static class RegularExpressions
    {
        public const string countryName = @"^[a-zA-Z]+(?:\s[a-zA-Z]+)*$";
        public const string countryCode = @"^[a-zA-Z0-9\s]*[a-zA-Z0-9]$";
        public const string description = @"^(?:[^\s]|[^\s].*[^\s])$";
        public const string target = @"^(^100([.]0{1,2})?)$|(^\d{1,2}([.]\d{1,2})?)$";

    }
}