using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatBot_Repo.Utils
{
    public static class ValidationUtils
    {
        public static bool IsStringValid(string testStr,string regex) {
            if (testStr == null || regex == null) return false;
            if (!Regex.IsMatch(testStr, regex)) return false;
            return true;
        }
    }
}
