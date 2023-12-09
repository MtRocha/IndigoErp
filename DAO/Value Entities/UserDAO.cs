using IndigoErp.Models;
using System.Data;

namespace IndigoErp.DAO
{
    public class UserDAO : GeneralDAO
    {
        protected UserModel CreateObject(DataRow row)
        {
            var user = new UserModel();

            user.Email = row["EMAIL"].ToString();
            user.Cnpj = row["CNPJ"].ToString();
            user.Password = row["SENHA"].ToString();
            user.SecurityCode = row["CODIGO_SEGURANCA"].ToString();

            return user;
        }
    }
}