using IndigoErp.Models;
using System.Data;

namespace IndigoErp.DAO
{
    public class SetorDAO : GeneralDAO
    {
        public List<string> ListSections(string cnpj)
        {
            List<string> list = new List<string>();
            QueryModel query = new QueryModel("SETORES", "CNPJ_DOMINIO", cnpj);
            DataTable table = Query(query);

            foreach (DataRow dr in table.Rows)
            {
                list.Add(dr["NOME"].ToString());
            }
            return list;
        }
    }
}