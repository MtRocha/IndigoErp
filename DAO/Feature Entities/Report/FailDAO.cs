using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class FailDAO
    {
        private SqlParameter[] CriaParametros(FailModel falha)
        {
            SqlParameter[] parametros = new SqlParameter[5];
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

            parametros[4] = new SqlParameter("cnpj", falha.Cnpj.ToUpper());

            return parametros;
        }

        private FailModel MontaFalha(DataRow falha)
        {
            FailModel f = new FailModel();

            f.Id = Convert.ToInt32(falha["ID"]);
            f.Origem = Convert.ToString(falha["ORIGEM"]);
            f.Componente = Convert.ToString(falha["NUMERO_EQUIPAMENTO"]);
            f.Tipo = Convert.ToString(falha["CAUSA_DA_FALHA"]);
            return f;
        }

        public List<FailModel> Consulta(string cnpj)
        {
            List<FailModel> list = new List<FailModel>();

            string consulta = "SELECT * FROM TIPO_DE_FALHA WHERE CNPJ =" + cnpj;

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow falha in table.Rows)
            {
                list.Add(MontaFalha(falha));
            }

            return list;
        }

        public void Inserir(FailModel falha)
        {
                string insercao =  "INSERT INTO TIPO_DE_FALHA (ORIGEM,NUMERO_EQUIPAMENTO,CAUSA_DA_FALHA,CNPJ)" +
                 "VALUES(@origem,@componente,@tipo,@cnpj)";
                GeneralDAO.ExecutaSql(insercao, CriaParametros(falha));
            

        }

        public void Excluir(int id)
        {
            string remocao = $"DELETE TIPO_DE_FALHA WHERE ID =" + id;
            GeneralDAO.ExecutaSql(remocao, null);
        }

        public FailModel ConsultaPorId(int id)
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

        public void AlterarFalha(FailModel falha, int id)
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