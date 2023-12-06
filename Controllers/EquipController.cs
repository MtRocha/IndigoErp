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
            List<EquipModel> list = equipService.ListEquip("*","NOME","ASC");
            return View(list);
        }

        public IActionResult CreateEquip()
        {
            ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
            return View();
        }

        public IActionResult EditEquip(int id) 
        {

            EquipModel model = equipService.GetEquip(id);

            ViewBag.Mode = "U";

            ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));

            return View("CreateEquip" ,model);
        
        
        }

        public IActionResult DeleteEquip(int id) 
        {
            equipService.Delete(id);

           return RedirectToAction("Index");

        }

        public IActionResult InsertEquip(EquipModel model,string operation)
        {
            try
            {
                switch (equipService.ValidateEquip(model,operation))
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
                if (ViewBag.Mode == "U")
                    equipService.Insert(model);
                else
                    equipService.Edit(model);
                    return RedirectToAction("Index");
                }
            catch (Exception ex) 
            {
                return View("Error", new ErrorViewModel());
            }
        }


        
    }
}
