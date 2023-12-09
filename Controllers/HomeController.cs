using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class HomeController : GeneralController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}