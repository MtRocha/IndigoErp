using System.Text.RegularExpressions;

namespace IndigoErp.Services
{
    public class ValidationService
    {
        public bool ValidateString(string value)
        {
            Regex regex = new Regex("^[^~\"\';:]$");

            if (regex.IsMatch(value) && !string.IsNullOrEmpty(value))
            { 
                return true;
            }
            else { return false; }
        
        }

        public bool ValidateInt(int value,bool isLowerThanZero,bool isEqualToZero)
        {
        
            if ((value == 0 && !isEqualToZero) || (value < 0 && !isLowerThanZero)) 
            {
                return true;
            }
            else{ return false; }
        
        }

    }
}
