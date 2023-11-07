using F.E.R.A_1._0.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;

namespace IndigoErp.DAO
{
    public class PadraoDAO
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

        }
    }
