using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class EquipController : Controller
    {
        private EquipService equipService = new EquipService();
        private SetorService setorService = new SetorService();
        private ValidationService validationService = new ValidationService();
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
                validationService.ValidateEquip(model,operation);
                if (!ModelState.IsValid)
                {
                    ViewBag.Mode = operation;
                    setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                    return View("CreateEquip",model);
                }
                else
                {
                    ViewBag.Ok = "ok";
                    model.Cnpj = HttpContext.Session.GetString("cnpj");
                    if (operation != "U")
                    {
                        equipService.Insert(model);
                    }
                    else
                    {
                        equipService.Edit(model);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel());
            }
        }


        
    }
}
