using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class GeneralDAO
        {
        private GeneralDAO()
            { }

        public static void ExecutaSql(string comando, SqlParameter[] parametro)
            {
            using (SqlConnection conexao = ConectarDAO.ConexaoDB())
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
            using (SqlConnection conexao = ConectarDAO.ConexaoDB())
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