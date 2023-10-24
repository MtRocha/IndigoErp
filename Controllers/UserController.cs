using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
