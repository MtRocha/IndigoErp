using System.Data.SqlClient;

namespace F.E.R.A_1._0.DAO
{
    public class ConectarDAO

    {
        private ConectarDAO()
        { }

        public static SqlConnection ConexaoDB()
        {


            //string connectingCode = "Data Source=LAB; Initial Catalog=INDIGODB;User=sa;Password=123456";

            //string connectingCode = "Data Source=LEV; Initial Catalog=INDIGODB;User=sa;Password=123456";

            string connectingCode = "Data Source=LAB3N-18\\SQLEXPRESS; Initial Catalog=INDIGODB;Integrated Security=true";

            SqlConnection conexao = new SqlConnection(connectingCode);
            conexao.Open();
            return conexao;
        }
    }
}