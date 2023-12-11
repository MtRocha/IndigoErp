using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace IndigoErp.Services
{
    public class ValidationService : Controller
    {
        public bool ValidateString(string value)
        {
            Regex regex = new Regex("^[\"\';:]$");

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            if (regex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateInt(int value, bool isLowerThanZero, bool isEqualToZero)
        {
            if ((value == 0 && !isEqualToZero) || (value < 0 && !isLowerThanZero))
            {
                return true;
            }
            else { return false; }
        }
    }
}