using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class ConectDAO

    {
        private ConectDAO()
        { }

        public static SqlConnection ConexaoDB()
        {
            //Url Pc Matheus
            string connectingCode = "Data Source=NOTE_VW; Initial Catalog=INDIGODB;Integrated Security=true";
            //Url do Pc 19
            //string connectingCode = "Data Source=LAB3N-19\\SQLEXPRESS; Initial Catalog=INDIGODB;Integrated Security=true";
            //Url do Pc 18
            //string connectingCode = "Data Source=LAB3N-18\\SQLEXPRESS; Initial Catalog=INDIGODB;Integrated Security=true";

            SqlConnection conexao = new SqlConnection(connectingCode);
            conexao.Open();
            return conexao;
        }
    }
}