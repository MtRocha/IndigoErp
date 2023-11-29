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
                switch (equipService.ValidateEquip(model))
                {
                    case "stringError":
                        ModelState.AddModelError("Id", "Caracteres Inválidos nos Campos Abaixo");
                        ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                        return View("CreateEquip", model);
                    break;

                    case "similarFound":
                        ModelState.AddModelError("Id", "Equipamento Já Existe");
                        ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                        return View("CreateEquip", model);
                    break;
                    case "sectionNotChosed":
                        ModelState.AddModelError("Id", "Escolha um Setor Para o Equipamento");
                        ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                        return View("CreateEquip", model);
                    break;
                }
                    ViewBag.Ok = "ok";
                    model.Cnpj = HttpContext.Session.GetString("cnpj");
                    equipService.Insert(model);
                    return View("Index");
                }
            catch (Exception ex) 
            {
                return View("Error", new ErrorViewModel());
            }
        }


        
    }
}
