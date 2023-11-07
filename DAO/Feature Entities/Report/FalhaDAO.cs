using F.E.R.A_1._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
    {
    public class FalhaDAO
        {
        private SqlParameter[] CriaParametros(FalhaViewModel falha)
            {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", falha.Id);
            parametros[1] = new SqlParameter("origem", falha.Origem.ToUpper());

            parametros[2] = new SqlParameter("componente", falha.Componente.ToUpper());
            if (falha.Tipo == null)
                {
                parametros[3] = new SqlParameter("tipo", DBNull.Value);
                }
            else
                {
                parametros[3] = new SqlParameter("tipo", falha.Tipo.ToUpper());
                }

            return parametros;
            }

        private FalhaViewModel MontaFalha(DataRow falha)
            {
            FalhaViewModel f = new FalhaViewModel();

            f.Id = Convert.ToInt32(falha["ID"]);
            f.Origem = Convert.ToString(falha["ORIGEM_DA_FALHA"]);
            f.Componente = Convert.ToString(falha["TIPO_DE_COMPONENTE"]);
            f.Tipo = Convert.ToString(falha["CAUSA_DA_FALHA"]);
            return f;
            }


        public List<FalhaViewModel> Consulta()
            {
            List<FalhaViewModel> list = new List<FalhaViewModel>();

            string consulta = "SELECT DISTINCT" +
             " cf.ID, cf.ORIGEM_DA_FALHA, cf.TIPO_DE_COMPONENTE, tf.CAUSA_DA_FALHA,tf.ID " +
             "FROM " +
             "COMPONENTE_DE_FALHA cf " +
             "FULL JOIN " +
             "TIPO_DE_FALHA tf ON cf.ID = tf.ID ORDER BY ORIGEM_DA_FALHA,TIPO_DE_COMPONENTE,CAUSA_DA_FALHA";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
                {
                list.Add(MontaFalha(falha));
                }

            return list;
            }

        public void Inserir(FalhaViewModel falha)
            {
            if (falha.Origem == "INTERNA")
                {
                string insercao = "INSERT INTO COMPONENTE_DE_FALHA (ID,ORIGEM_DA_FALHA, TIPO_DE_COMPONENTE)" +
                "VALUES (@id,@origem, @componente)" +
                "INSERT INTO TIPO_DE_FALHA (ID,COMPONENTE,CAUSA_DA_FALHA)" +
                 "VALUES(@id,@componente,@tipo)";
                GeneralDAO.ExecutaSql(insercao, CriaParametros(falha));
                }
            else
                {
                string insercao = "INSERT INTO COMPONENTE_DE_FALHA (ID,ORIGEM_DA_FALHA, TIPO_DE_COMPONENTE)" +
        "VALUES (@id,@origem, @componente)";
                GeneralDAO.ExecutaSql(insercao, CriaParametros(falha));
                }
            }

        public void Excluir(int id)
            {
            string remocao = $"DELETE COMPONENTE_DE_FALHA WHERE ID =" + id +
                             $"DELETE TIPO_DE_FALHA WHERE ID =" + id;
            GeneralDAO.ExecutaSql(remocao, null);
            }

        public FalhaViewModel ConsultaPorId(int id)
            {
            string consulta = " SELECT cf.ID, cf.ORIGEM_DA_FALHA, cf.TIPO_DE_COMPONENTE, tf.CAUSA_DA_FALHA " +
                                " FROM " +
                                " COMPONENTE_DE_FALHA cf " +
                                " LEFT JOIN " +
                                " TIPO_DE_FALHA tf ON cf.ID = tf.ID WHERE cf.ID = " + id;

            DataTable tabela = GeneralDAO.SelectSql(consulta, null);

            if (tabela.Rows.Count == 0)
                {
                return null;
                }
            else
                {
                return MontaFalha(tabela.Rows[0]);
                }
            }

        public void AlterarFalha(FalhaViewModel falha, int id)
            {
            if (falha.Origem == "EXTERNA")
                {
                string alteracao = " UPDATE COMPONENTE_DE_FALHA " +
                             " SET TIPO_DE_COMPONENTE = @componente," +
                             " ORIGEM_DA_FALHA = @origem, " +
                             " ID = @id " +
                             " WHERE ID = " + id +
                             " DELETE TIPO_DE_FALHA WHERE ID = " + id;

                GeneralDAO.ExecutaSql(alteracao, CriaParametros(falha));
                }
            else
                {
                string alteracao = " UPDATE COMPONENTE_DE_FALHA " +
                               " SET TIPO_DE_COMPONENTE = @componente, " +
                                " ORIGEM_DA_FALHA = @origem, " +
                                " ID = @id " +
                                " WHERE ID = " + id +
                                " UPDATE  TIPO_DE_FALHA " +
                                " SET COMPONENTE = @componente, " +
                                " CAUSA_DA_FALHA = @tipo WHERE ID = " + id +
                                "INSERT INTO TIPO_DE_FALHA (ID,COMPONENTE,CAUSA_DA_FALHA)" +
                                "VALUES(@id,@componente,@tipo)";

                GeneralDAO.ExecutaSql(alteracao, CriaParametros(falha));
                }
            }

        public int SugerirId()
            {
            string sql = "SELECT ISNULL (MAX(ID)+1,1) AS 'MAIOR' FROM COMPONENTE_DE_FALHA";
            DataTable tabela = GeneralDAO.SelectSql(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
            }

        public List<string> ConsultaComponente(string texto)
            {
            List<string> list = new List<string>();

            string consulta = $"SELECT DISTINCT TIPO_DE_COMPONENTE FROM COMPONENTE_DE_FALHA WHERE ORIGEM_DA_FALHA = '{texto}'";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
                {
                list.Add(Convert.ToString(falha["TIPO_DE_COMPONENTE"]));
                }

            return list;
            }

        public List<string> ConsultaCausa(string texto)
            {
            List<string> list = new List<string>();

            string consulta = $"SELECT DISTINCT CAUSA_DA_FALHA FROM TIPO_DE_FALHA WHERE COMPONENTE = '{texto}'";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
                {
                list.Add(Convert.ToString(falha["CAUSA_DA_FALHA"]));
                }

            return list;
            }
        }
    }