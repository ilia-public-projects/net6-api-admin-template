using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services.Utils
{
    public static class PasswordValidationUtils
    {
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if
            (
                password.Length >= 6 &&        //if length is >= 6
                password.Any(char.IsUpper) &&  //if any character is upper case
                password.Any(char.IsLower) &&  //if any character is lower case
                password.Any(char.IsNumber)    //if any character is number
            )
            {
                return true;
            }

            return false;
        }
    }
}
