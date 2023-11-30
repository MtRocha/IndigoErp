using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO.Value_Entities
{
    public class EquipDAO : GeneralDAO
    {
        private SqlParameter[] CreateParameters(EquipModel model)
        {

            SqlParameter[] parametros = new SqlParameter[7];

            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("numeroSerie", model.NumeroSerie);
            parametros[2] = new SqlParameter("nome", model.Nome);
            parametros[3] = new SqlParameter("setor", model.Setor);
            parametros[4] = new SqlParameter("cnpj", model.Cnpj);
            parametros[5] = new SqlParameter("modelo", model.Modelo);
            parametros[6] = new SqlParameter("marca", model.Marca);

            return parametros;
        }

        private EquipModel CreateObject(DataRow row)
        { 
        
            EquipModel model = new EquipModel();

            model.Nome = row["NOME"].ToString();
            model.Setor = row["SETOR"].ToString();
            model.Cnpj = row["CNPJ_DOMINIO"].ToString();
            model.NumeroSerie = row["NUMERO_DE_SERIE"].ToString();
            model.Modelo = row["MODELO"].ToString();
            model.Marca = row["MARCA"].ToString();

            return model;
        }

        public void Insert(EquipModel model)
        {
            string sql = "INSERT INTO EQUIPAMENTO( " +
                         "NOME, " +
                         "NUMERO_DE_SERIE, " +
                         "CNPJ_DOMINIO, " +
                         "SETOR," +
                         "MODELO," +
                         "MARCA)" +
                         "VALUES (" +
                         "@nome, " +
                         "@numeroSerie, " +
                         "@cnpj, " +
                         "@setor," +
                         "@marca," +
                         "@modelo )";

            GeneralDAO.ExecutaSql(sql, CreateParameters(model));

        }

        public List<EquipModel> EquipQuery(string query)
        {

            DataTable table = Listing("EQUIPAMENTO", query);
            List<EquipModel> list = new List<EquipModel>();

            if(table != null)
            {
                foreach (DataRow item in table.Rows)
                {
                    list.Add(CreateObject(item));
                }

                return list;
            }
            else
            {
                return null;
            }
            
        }

        public void Update(EquipModel model)
        {
            string sql = "UPDATE EQUIPAMENTO" +
                         "SET NOME = @nome, " +
                         "NUMERO_DE_SERIE = @numeroSerie, " +
                         "CNPJ_DOMINIO = @cnpj, " +
                         "SETOR = @setor";

            GeneralDAO.ExecutaSql(sql, CreateParameters(model));

        }

        public bool SearchSimilar(string serialNumber)
        {

            DataTable table = Query("EQUIPAMENTO", "NUMERO_DE_SERIE", serialNumber);

            return table != null ? true : false;
        }

    }
}
