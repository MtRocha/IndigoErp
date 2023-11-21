 using F.E.R.A_1._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
    {
    public class ReportDAO : GeneralDAO
        {
        private SqlParameter[] CriaParametros(ReportModel report)
            {
            SqlParameter[] parametros = new SqlParameter[12];
            if (report.NumeroTeste == null)
                {
                parametros[0] = new SqlParameter("numeroTeste", DBNull.Value);
                }
            else
                {
                parametros[0] = new SqlParameter("numeroTeste", report.NumeroTeste);
                }

            parametros[1] = new SqlParameter("celula", report.Celula);

            parametros[2] = new SqlParameter("idFuncionario", report.IdFuncionario);

            parametros[3] = new SqlParameter("origem", report.Origem.ToUpper());

            parametros[4] = new SqlParameter("componente", report.Componente.ToUpper());

            if (report.Tipo == "Causa" || report.Origem == "EXTERNA")
                {
                parametros[5] = new SqlParameter("tipo", DBNull.Value);
                }
            else
                {
                parametros[5] = new SqlParameter("tipo", report.Tipo.ToUpper());
                }
            parametros[6] = new SqlParameter("data", report.Data);

            parametros[7] = new SqlParameter("inicio", report.Inicio);

            if (report.Descricao == null)
                {
                parametros[8] = new SqlParameter("descricao", DBNull.Value);
                }
            else
                {
                parametros[8] = new SqlParameter("descricao", report.Descricao);
                }

            parametros[9] = new SqlParameter("final", report.Final);

            parametros[10] = new SqlParameter("status", report.Status);

            parametros[11] = new SqlParameter("dataFinal", report.DataFinal);

            return parametros;
            }

         new ReportModel MontaObjeto<T>(DataRow report)
        {

            var r = new ReportModel();

            r.Id = Convert.ToInt32(report["ID"]);
            r.NumeroTeste = report["NUMERO_TESTE"].ToString();
            r.IdFuncionario = Convert.ToInt32(report["ID_FUNCIONARIO"]);
            r.Celula = report["CELULA_DE_REPORT"].ToString();
            r.Origem = Convert.ToString(report["ORIGEM_REPORT"]);
            r.Componente = Convert.ToString(report["COMPONENTE_DE_FALHA"]);
            r.Tipo = Convert.ToString(report["TIPO_DE_FALHA"]);
            r.Descricao = Convert.ToString(report["DESCRICAO"]);
            r.Data = Convert.ToDateTime(report["DATA_DA_OCORRENCIA"].ToString());
            r.Inicio = Convert.ToDateTime(report["INICIO"].ToString());
            r.Final = Convert.ToDateTime(report["FINAL"].ToString());
            r.DataFinal = Convert.ToDateTime(report["DATA_FINAL"]);
            r.Status = (report["STATUS"]).ToString();

            return r;
            }

        public List<ReportModel> Consulta()
            {
            List<ReportModel> list = new List<ReportModel>();

            string consulta = "SELECT * FROM REPORTS WHERE STATUS = 'FINALIZADO' AND DATA_DA_OCORRENCIA >= DATEADD(DAY,-7,GETDATE()) ORDER BY DATA_DA_OCORRENCIA DESC";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
                {
                list.Add(MontaObjeto<ReportModel>(falha));
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
                list.Add(MontaObjeto<ReportModel>(falha));
                }

            return list;
            }


        public void Inserir(ReportModel report)
            {
            string insercao = "INSERT INTO REPORTS (NUMERO_TESTE,CELULA_DE_REPORT,ID_FUNCIONARIO,ORIGEM_REPORT,COMPONENTE_DE_FALHA,TIPO_DE_FALHA,DESCRICAO,DATA_DA_OCORRENCIA,INICIO,FINAL,STATUS,DATA_FINAL)" +
            "VALUES (@numeroTeste,@celula,@idFuncionario,@origem,@componente,@tipo,@descricao,@data,@inicio,@final,@status,@dataFinal)";
            GeneralDAO.ExecutaSql(insercao, CriaParametros(report));
            }

        public void Excluir(int id)
            {
            string remocao = $"DELETE REPORTS WHERE ID =" + id;
            GeneralDAO.ExecutaSql(remocao, null);
            }

        public List<ReportModel> ConsultaPendente()
            {
            List<ReportModel> list = new List<ReportModel>();

            string consulta = "SELECT * FROM REPORTS WHERE STATUS = 'PENDENTE' ";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
                {
                list.Add(MontaObjeto<ReportModel>(falha));
                }

            return list;
            }

        public ReportModel ConsultaPorId(int id)
            {
            string consulta = "SELECT * FROM REPORTS WHERE ID = " + id;

            DataTable tabela = GeneralDAO.SelectSql(consulta, null);

            if (tabela.Rows.Count == 0)
                {
                return null;
                }
            else
                {
                return MontaObjeto<ReportModel>(tabela.Rows[0]);
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
                    list.Add(MontaObjeto<ReportModel>(item));
                    }
                return list;
                }
            }

        
        


    public void AlterarReport(ReportModel report, int id)
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