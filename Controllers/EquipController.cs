using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class EquipController : Controller
    {
        private EquipService equipService = new EquipService();
        private SetorService setorService = new SetorService();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEquip()
        {
            ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
            return View();
        }

        public IActionResult InsertEquip(EquipModel model)
        {
            try
            {
              
                if (equipService.ValidateEquip(model) == "stringError")
                {
                    ModelState.AddModelError("Id", "Caracteres Inválidos nos Campos Abaixo");
                    ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                    return View("CreateEquip", model);
                }
                else if (equipService.ValidateEquip(model) == "similarFound")
                {
                    ModelState.AddModelError("Id", "Caracteres Inválidos nos Campos Abaixo");
                    ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                    return View("CreateEquip",model);
                }
                else
                {
                    ViewBag.Ok = "ok";
                    equipService.Insert(model);
                    return View();
                }
            }
            catch (Exception ex) 
            {
                return View("Error", ex.ToString());
            }
        }


        
    }
}
