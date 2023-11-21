using System;

namespace F.E.R.A_1._0.Models
    {
    public class ReportModel : PadraoViewModel
        {
        public int IdFuncionario { get; set; }
        public string Celula { get; set; }
        public string Origem { get; set; }
        public string Componente { get; set; }
        public string Tipo { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataFinal { get; set; }
        public string Descricao { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Final { get; set; }
        public string NumeroTeste { get; set; }

        public string Status { get; set; }
        }
    }