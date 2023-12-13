using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class HomeController : GeneralController
    {
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}