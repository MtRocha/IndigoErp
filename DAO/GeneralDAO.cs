using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class GeneralDAO
        {

        internal SqlParameter[] CreateQuery(string tabela, string coluna, string id)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@tabela", tabela);
            parametros[1] = new SqlParameter("@coluna", tabela);
            parametros[2] = new SqlParameter("@id", tabela);

            return parametros;
        }


        internal DataTable Listing<T>(string tabela, string coluna)
        {
            string sql = $"EXEC spQuery '{tabela}' , '{coluna}'  ";
            DataTable table = GeneralDAO.SelectSql(sql, CreateQuery(tabela, coluna, ""));

            if (table.Rows.Count != 0)
            {
                return table;
            }
            else
            {
                return null;
            }

        }

    
    public static void ExecutaSql(string comando, SqlParameter[] parametro)
            {
            using (SqlConnection conexao = ConectDAO.ConexaoDB())
                {
                using (SqlCommand cmd = new SqlCommand(comando, conexao))
                    {
                    if (parametro != null)
                        {
                        cmd.Parameters.AddRange(parametro);
                        }

                    cmd.ExecuteNonQuery();

                    }
                }
            }

        public static DataTable SelectSql(string sql, SqlParameter[] parametro)
            {
            using (SqlConnection conexao = ConectDAO.ConexaoDB())
                {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                    {
                    if (parametro != null)
                        adapter.SelectCommand.Parameters.AddRange(parametro);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    conexao.Close();
                    return table;
                    }
                }
            }
        }
    }