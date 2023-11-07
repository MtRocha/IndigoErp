
using IndigoErp.DAO;
using IndigoErp.Models;
using System.Data;
using System.Data.SqlClient;

namespace IndigoErp.DAO
{
    public class LoginDAO : UserDAO
    {
        public UserModel VerifyUser(LoginModel login)
        {
            string sql = $"EXECUTE SPCONSULTA 'USUARIOS','EMAIL',\"'{login.Email}'\"";

            DataTable table = GeneralDAO.SelectSql(sql,null);

            if (table.Rows.Count == 0) 
            { 
              return null;
              
            }
            else
            {

                DataRow row = table.Rows[0];
                if (row["SENHA"].ToString() == login.Password)
                {
                    return null;
                }
                else 
                {
                    return CreateObject(row); ; 
                }
            }
        }

    }
}
