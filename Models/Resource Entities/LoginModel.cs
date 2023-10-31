namespace IndigoErp.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public LoginModel(string Email,string Password) 
        {
          Email = this.Email;
          Password = this.Email; 
        
        }

    }
}
