using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class ConectarDAO

    {
        private ConectarDAO()
        { }

        public static SqlConnection ConexaoDB()
        {

            //string connectingCode = "Data Source=LAB3N-19\\SQLEXPRESS; Initial Catalog=INDIGODB;Integrated Security=true";

            string connectingCode = "Data Source=LAB3N-18\\SQLEXPRESS; Initial Catalog=INDIGODB;Integrated Security=true";

            SqlConnection conexao = new SqlConnection(connectingCode);
            conexao.Open();
            return conexao;
        }
    }
}