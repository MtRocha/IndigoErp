using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class GeneralDAO
        {

        internal static SqlParameter[] CreateQuery(string tabela, string coluna, string id)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@tabela", tabela);
            parametros[1] = new SqlParameter("@coluna", tabela);
            parametros[2] = new SqlParameter("@id", tabela);

            return parametros;
        }


        internal DataTable Query(string table, string column,string id)
        {
            string sql = $"EXEC SPCONSULTA '{table}' , '{column}','{id}'  ";
            DataTable result = GeneralDAO.SelectSql(sql, CreateQuery(table, column, id));

            if (result.Rows.Count != 0)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        public DataTable Listing(string table, string column)
        {
            string sql = $"EXEC SPLISTAGEM '{table}' , '{column}'";
            DataTable result = GeneralDAO.SelectSql(sql, CreateQuery(table, column, ""));

            if (result.Rows.Count != 0)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        public DataTable Listing(string table, string column,string filter,string order)
        {
            string sql = $"EXEC SPLISTAGEM '{table}' , '{column}','{filter}','{order}'";
            DataTable result = GeneralDAO.SelectSql(sql, null);

            if (result.Rows.Count != 0)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        public void Delete(string table, int id)
        {
        
            string sql = $"EXEC SPDELETE '{table}' , '{id}'";

            ExecutaSql(sql, null);

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