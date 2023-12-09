namespace IndigoErp.Models
{
    public class UserModel : PadraoViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public string SecurityCode { get; set; }
    }
}