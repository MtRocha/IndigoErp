using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IndigoErp.Controllers
{
    public class GeneralController : Controller
    {

        public bool VerifyCredentials()
        {
            string verification = HttpContext.Session.GetString("Logged");

            bool result = verification == null ? false : true;

            return result;
           
        }

    }
}
