using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class OcorrenciasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReport() 
        {
        
            return View();
        
        }
    }
}
