using IndigoErp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics;

namespace IndigoErp.Controllers
{
    public class HomeController : GeneralController
    {

        public IActionResult Index()
        {
            if (!VerifyCredentials())
                return RedirectToAction("Index", "User");
            else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}