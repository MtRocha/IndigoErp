using IndigoErp.DAO;
using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class EquipController : Controller
    {
        private EquipService equipService = new EquipService();
        private SetorService setorService = new SetorService();
        private ValidationService val = new ValidationService();

        public bool ValidateEquip(EquipModel model, string operation)
        {
            ModelState.Clear();

            EquipDAO dao = new EquipDAO();

            if (!val.ValidateString(model.Marca))
            {
                ModelState.AddModelError("Marca", "Caractéres Inválidos");
                return false;
            }
            if (!val.ValidateString(model.Modelo))
            {
                ModelState.AddModelError("Modelo", "Caractéres Inválidos");
                return false;
            }
            if (!val.ValidateString(model.Nome))
            {
                ModelState.AddModelError("Nome", "Caractéres Inválidos");
                return false;
            }
            if (!val.ValidateString(model.NumeroSerie))
            {
                ModelState.AddModelError("NumeroSerie", "Caractéres Inválidos");
                return false;
            }
            if (model.Setor == "Setor")
            {
                ModelState.AddModelError("Setor", "Escolha Um setor Para o Equipamento");
                return false;
            }
            if (dao.SearchSimilar(model.NumeroSerie) && operation != "U")
            {
                ModelState.AddModelError("Id", "Equipamento já Existe");
                return false;
            }

            return true;
        }

        public IActionResult Index()
        {
            List<EquipModel> list = equipService.ListEquip("*", "NOME", "ASC");
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

            return View("CreateEquip", model);
        }

        public IActionResult DeleteEquip(int id)
        {
            equipService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult InsertEquip(EquipModel model, string operation)
        {
            try
            {
                if (!ValidateEquip(model, operation))
                {
                    ViewBag.Mode = operation;
                    ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                    return View("CreateEquip", model);
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