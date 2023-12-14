using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class ReportDAO : GeneralDAO
    {
        private SqlParameter[] CriaParametros(ReportModel report)
        {
            SqlParameter[] parametros = new SqlParameter[13];
            if (report.MaintenceTYpe == null)
            {
                parametros[0] = new SqlParameter("tipoManutencao", DBNull.Value);
            }
            else
            {
                parametros[0] = new SqlParameter("tipoManutencao", report.MaintenceTYpe);
            }

            parametros[1] = new SqlParameter("celula", report.Section);

            parametros[2] = new SqlParameter("idFuncionario", report.EmployeeId);


            parametros[3] = new SqlParameter("origem", report.Origin.ToUpper());

            parametros[4] = new SqlParameter("componente", report.FailType.ToUpper());

            if (report.FailCause == "Causa" || report.Origin == "EXTERNA")
            {
                parametros[5] = new SqlParameter("tipo", DBNull.Value);
            }
            else
            {
                parametros[5] = new SqlParameter("tipo", report.FailCause.ToUpper());
            }
            parametros[6] = new SqlParameter("data", report.InitialDate);

            parametros[7] = new SqlParameter("inicio", report.Begin);

            if (report.Description == null)
            {
                parametros[8] = new SqlParameter("descricao", DBNull.Value);
            }
            else
            {
                parametros[8] = new SqlParameter("descricao", report.Description);
            }

            parametros[9] = report.End == null ? new SqlParameter("final", report.End) : new SqlParameter("final", DBNull.Value);

            parametros[10] = new SqlParameter("status", report.Status);

            parametros[11] = report.FinalDate == null ? new SqlParameter("dataFinal", report.FinalDate) : new SqlParameter("dataFinal", DBNull.Value);

            parametros[12] = new SqlParameter("cnpj", report.Cnpj);

            return parametros;
        }

        public ReportModel CreateObject(DataRow report)
        {
            var r = new ReportModel();

            r.Id = Convert.ToInt32(report["ID"]);
            r.MaintenceTYpe = report["TIPO_MANUTENCAO"].ToString();
            r.EmployeeId = report["ID_FUNCIONARIO"].ToString();
            r.Section = report["SETOR"].ToString();
            r.Origin = Convert.ToString(report["ORIGEM_REPORT"]);
            r.FailType = Convert.ToString(report["COMPONENTE_DE_FALHA"]);
            r.FailCause = Convert.ToString(report["TIPO_DE_FALHA"]);
            r.Description = Convert.ToString(report["DESCRICAO"]);
            r.InitialDate = Convert.ToDateTime(report["DATA_DA_OCORRENCIA"].ToString());
            r.Begin = Convert.ToDateTime(report["INICIO"].ToString());
            r.End = Convert.ToDateTime(report["FINAL"].ToString());
            r.FinalDate = Convert.ToDateTime(report["DATA_FINAL"]);
            r.Status = (report["STATUS"]).ToString();

            return r;
        }

        public List<ReportModel> Consulta(string cnpj)
        {
            List<ReportModel> list = new List<ReportModel>();

            string consulta = $"SELECT * FROM REPORTS WHERE STATUS = 'FINALIZADO' AND DATA_DA_OCORRENCIA >= DATEADD(DAY,-7,GETDATE()) AND CNPJ_ORIGEM = '{cnpj}' ORDER BY DATA_DA_OCORRENCIA DESC";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
            {
                list.Add(CreateObject(falha));
            }

            return list;
        }

        public List<ReportModel> ConsultaGeral()
        {
            List<ReportModel> list = new List<ReportModel>();

            string consulta = "SELECT * FROM REPORTS WHERE DATA_DA_OCORRENCIA >= DATEADD(DAY,-7,GETDATE()) ORDER BY DATA_DA_OCORRENCIA DESC";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
            {
                list.Add(CreateObject(falha));
            }

            return list;
        }

        public void Insert(ReportModel report)
        {
            string insercao = "INSERT INTO REPORTS (TIPO_MANUTENCAO,SETOR,ID_FUNCIONARIO,ORIGEM_REPORT,COMPONENTE_DE_FALHA,TIPO_DE_FALHA,DESCRICAO,DATA_DA_OCORRENCIA,INICIO,FINAL,STATUS,DATA_FINAL,CNPJ_ORIGEM)" +
            "VALUES (@tipoManutencao,@celula,@idFuncionario,@origem,@componente,@tipo,@descricao,@data,@inicio,@final,@status,@dataFinal,@cnpj)";
            GeneralDAO.ExecutaSql(insercao, CriaParametros(report));
        }

        public void Excluir(int id)
        {
            string remocao = $"DELETE REPORTS WHERE ID =" + id;
            GeneralDAO.ExecutaSql(remocao, null);
        }

        public List<ReportModel> ConsultaPendente(string cnpj)
        {
            List<ReportModel> list = new List<ReportModel>();

            string consulta = $aqa"SELECT * FROM REPORTS WHERE STATUS = 'PENDENTE' AND CNPJ_ORIGEM = '{cnpj}' ";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
            {
                list.Add(CreateObject(falha));
            }

            return list;
        }

        public ReportModel SearchReport(int id)
        {
            string consulta = "SELECT * FROM REPORTS WHERE ID = " + id;

            DataTable tabela = GeneralDAO.SelectSql(consulta, null);

            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return CreateObject(tabela.Rows[0]);
            }
        }

        public List<ReportModel> ListagemPorNumTeste(string teste)
        {
            var list = new List<ReportModel>();

            string consulta = $"SELECT * FROM REPORTS WHERE NUMERO_TESTE = '{teste}'";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            if (table.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow item in table.Rows)
                {
                    list.Add(CreateObject(item));
                }
                return list;
            }
        }

        public void Update(ReportModel report, int id)
        {
            string alteracao = "UPDATE[REPORTS]" +
                               "SET FINAL = @final , " +
                               "[DATA_FINAL] = @dataFinal ," +
                               "[DESCRICAO] = @descricao ," +
                               "[NUMERO_TESTE] = @numeroTeste ," +
                               "[CELULA_DE_REPORT] = @celula ," +
                               "[ORIGEM_REPORT] = @origem," +
                               "[DATA_DA_OCORRENCIA] = @data ," +
                               "[INICIO] = @inicio  ," +
                               "[STATUS] = @status," +
                               "[COMPONENTE_DE_FALHA] = @componente ," +
                               "[TIPO_DE_FALHA] = @tipo " +
                               $"WHERE ID = {id}";

            GeneralDAO.ExecutaSql(alteracao, CriaParametros(report));
        }
    }
}