namespace IndigoErp.Models
{ 
    public class EmployeeModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }

    }
}
