using IndigoErp.Models;
namespace IndigoErp.Models
{
    public class LoginModel : UserModel
    {

        public LoginModel(string Email,string Password) 
        {
          Email = this.Email;
          Password = this.Email; 
        
        }

    }
}
