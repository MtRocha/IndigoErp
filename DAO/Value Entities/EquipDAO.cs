using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO.Value_Entities
{
    public class EquipDAO : GeneralDAO
    {
        public SqlParameter[] CriaParametros(EquipModel model)
        {

            SqlParameter[] parametros = new SqlParameter[5];

            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("numeroSerie", model.NumeroSerie);
            parametros[2] = new SqlParameter("nome", model.Nome);
            parametros[3] = new SqlParameter("setor", model.Setor);
            parametros[4] = new SqlParameter("cnpj", model.Cnpj);

            return parametros;
        }

        public void Insert(EquipModel model)
        {
            string sql = "INSERT INTO EQUIPAMENTO( " +
                         "NOME, " +
                         "NUMERO_DE_SERIE, " +
                         "CNPJ_DOMINIO, " +
                         "SETOR )" +
                         "VALUES (" +
                         "@nome, " +
                         "@numeroSerie, " +
                         "@cnpj, " +
                         "@setor )";

            GeneralDAO.ExecutaSql(sql, CriaParametros(model));

        }

        public void Update(EquipModel model)
        {
            string sql = "UPDATE EQUIPAMENTO" +
                         "SET NOME = @nome, " +
                         "NUMERO_DE_SERIE = @numeroSerie, " +
                         "CNPJ_DOMINIO = @cnpj, " +
                         "SETOR = @setor";

            GeneralDAO.ExecutaSql(sql, CriaParametros(model));

        }

        public bool SearchSimilar(string serialNumber)
        {

            DataTable table = Query("EQUIPAMENTO", "NUMERO_DE_SERIE", serialNumber);

            return table != null ? true : false;
        }

    }
}
