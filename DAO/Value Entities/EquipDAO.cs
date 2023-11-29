using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO.Value_Entities
{
    public class EquipDAO : GeneralDAO
    {
        public SqlParameter[] CriaParametros(EquipModel model)
        {

            SqlParameter[] parametros = new SqlParameter[6];

            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("numeroSerie", model.NumeroSerie);
            parametros[3] = new SqlParameter("nome", model.Nome);
            parametros[4] = new SqlParameter("setor", model.Setor);
            parametros[5] = new SqlParameter("cnpj", model.Cnpj);

            return parametros;
        }

        public void Insert(EquipModel model)
        {
            string sql = "INSERT INTO EQUIPAMENTOS( " +
                         "NOME, " +
                         "NUMERO_DE_SERIE, " +
                         "CNPJ_DOMINIO, " +
                         "SETOR )" +
                         "VALUES (" +
                         "@nome, " +
                         "@numeroSerie, " +
                         "@cnpj, " +
                         "@setor";

            GeneralDAO.ExecutaSql(sql, CriaParametros(model));

        }

        public void Update(EquipModel model)
        {
            string sql = "UPDATE EQUIPAMENTOS " +
                         "SET NOME = @nome, " +
                         "NUMERO_DE_SERIE = @numeroSerie, " +
                         "CNPJ_DOMINIO = @cnpj, " +
                         "SETOR = @setor";

            GeneralDAO.ExecutaSql(sql, CriaParametros(model));

        }

        public bool SearchSimilar(string serialNumber)
        {

            DataTable table = Query("EQUIPAMENTOS", "NUMERO_DE_SERIE", serialNumber);

            return table.Rows.Count > 0 ? true : false;
        }

    }
}
