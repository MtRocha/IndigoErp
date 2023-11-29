using IndigoErp.DAO;
using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class SetorDAO : GeneralDAO
    {

        public List<string> ListSections(string cnpj)
        {
            List<string> list = new List<string>();
            DataTable table = Query("SETORES", "CNPJ_DOMINIO", $"{cnpj}");
        
            foreach (DataRow dr in table.Rows)
            {
                list.Add(dr["NOME"].ToString());
            }
            return list;
        }
           

    }
}
