using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class GeneralDAO
    {
        private static SqlParameter[] CreateQuery(QueryModel query)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@table", query.Table);
            parameters[1] = new SqlParameter("@column", query.Column);
            parameters[2] = string.IsNullOrEmpty(query.Id) ? new SqlParameter("@id", DBNull.Value) : new SqlParameter("@id", query.Id);
            parameters[3] = string.IsNullOrEmpty(query.Filter) ? new SqlParameter("@filter", DBNull.Value) : new SqlParameter("@filter", query.Filter);
            parameters[4] = string.IsNullOrEmpty(query.Order) ? new SqlParameter("@order", DBNull.Value) : new SqlParameter("@order", query.Order);

            foreach (var param in parameters)
            {
                Console.WriteLine($"{param.ParameterName}: {param.Value}");
            }

            return parameters;
        }

        public DataTable Query(QueryModel query)
        {
            string sql = $"EXEC SPCONSULTA @table , @column, @id ";
            DataTable result = GeneralDAO.SelectSql(sql, CreateQuery(query));

            if (result.Rows.Count != 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public DataTable Listing(QueryModel query)
        {
            string sql = $"EXEC SPLISTAGEM '{query.Table}' , '{query.Column}' , '{query.Filter}' ,'{query.Order}' ";
            DataTable result = GeneralDAO.SelectSql(sql, CreateQuery(query));

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