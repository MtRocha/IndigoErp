using F.E.R.A_1._0.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;

namespace F.E.R.A_1._0.DAO
    {
    public class PadraoDAO
        {
        internal SqlParameter[] CriaConsulta(string tabela, string coluna, string id)
        {
            SqlParameter[] parametros = new SqlParameter[3];

            parametros[0] = new SqlParameter("@tabela", tabela);
            parametros[1] = new SqlParameter("@coluna", tabela);
            parametros[2] = new SqlParameter("@id", tabela);

            return parametros;
        }

        internal T MontaObjeto<T>(DataRow data)
        {
            T model ;

            model = (T) data["ID"];

            return model;
        }


        internal List<T> Listagem<T>(string tabela, string coluna)
        {
           string sql = $"EXEC spQuery '{tabela}' , '{coluna}'  ";
           List<T> list = new List<T>();    
           DataTable table = GeneralDAO.SelectSql(sql, CriaConsulta(tabela, coluna, ""));

            if (table.Rows.Count != 0)
            {
                foreach (DataRow row in table.Rows) 
                {

                    list.Add(MontaObjeto<T>(row));
                
                }

                return list;
            }
            else 
            { 
                return null; 
            }

        }



        }
    }
