using IndigoErp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
    {
    public class OrigemFalhaDAO
        {

        private SqlParameter[] CriaParametros(OrigemFalhaViewModel model)
            {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("origem", model.Origem.ToUpper());
            return parametros;
            }

        private OrigemFalhaViewModel MontaSelect(DataRow origem)
            {
            OrigemFalhaViewModel f = new OrigemFalhaViewModel();

            f.Id = Convert.ToInt32(origem["ID"]);
            f.Origem = Convert.ToString(origem["ORIGEM"]);

            return f;
            }

        public List<OrigemFalhaViewModel> Consulta()
            {
            List<OrigemFalhaViewModel> list = new List<OrigemFalhaViewModel>();

            string consulta = "SELECT * FROM  COMPONENTE_DE_FALHA_SELECAO ORDER BY ORIGEM ASC";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow origem in table.Rows)
                {
                list.Add(MontaSelect(origem));
                }

            return list;
            }

        public void Inserir(OrigemFalhaViewModel model)
            {
            string insercao = "INSERT INTO COMPONENTE_DE_FALHA_SELECAO (ID,ORIGEM)" +
            "VALUES (@id,@origem)";
            GeneralDAO.ExecutaSql(insercao, CriaParametros(model));
            }

        public void Excluir(int id)
            {
            string remocao = $"DELETE COMPONENTE_DE_FALHA_SELECAO WHERE ID =" + id;

            GeneralDAO.ExecutaSql(remocao, null);
            }

        public OrigemFalhaViewModel ConsultaPorId(int id)
            {
            string consulta = " SELECT * FROM  COMPONENTE_DE_FALHA_SELECAO WHERE ID = " + id;

            DataTable tabela = GeneralDAO.SelectSql(consulta, null);

            if (tabela.Rows.Count == 0)
                {
                return null;
                }
            else
                {
                return MontaSelect(tabela.Rows[0]);
                }
            }

        public void Alterar(OrigemFalhaViewModel model, int id)
            {

            string alteracao = " UPDATE COMPONENTE_DE_FALHA_SELECAO " +
                                 "SET " +
                                 " ID = @id , " +
                                 " ORIGEM = @origem " +
                                 " WHERE ID = " + id;

            GeneralDAO.ExecutaSql(alteracao, CriaParametros(model));

            }

        public int SugerirId()
            {
            string sql = "SELECT ISNULL (MAX(ID)+1,1) AS 'MAIOR' FROM COMPONENTE_DE_FALHA_SELECAO";
            DataTable tabela = GeneralDAO.SelectSql(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
            }
        }

}
    