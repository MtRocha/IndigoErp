using IndigoErp.Models;
using System.Data;

namespace IndigoErp.DAO
{
    public class LoginDAO : UserDAO
    {
        public UserModel VerifyUser(LoginModel login)
        {
            QueryModel query = new QueryModel("USUARIOS", "EMAIL", login.Email);

            DataTable table = Query(query);

            if (table == null)
            {
                return null;
            }
            else
            {
                DataRow row = table.Rows[0];
                if (row["SENHA"].ToString() == login.Password)
                {
                    return CreateObject(row);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}