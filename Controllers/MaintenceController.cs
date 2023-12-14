using IndigoErp.Models;
using IndigoErp.Services;
using IndigoErp.DAO;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class MaintenceController : Controller
    {
        private FailService failService = new FailService();
        private EquipService equipService = new EquipService();

        #region Fail

        public bool ValidateFail(FailModel model, string Operacao)
        {


            ModelState.Clear();
            FailDAO dao = new FailDAO();

            if (model.Origem != "Falha Interna de Equipamento" && model.Origem != "Falha Externa da Operação")
            {
                ModelState.AddModelError("Origem", "Selecione uma Origem para a Falha");
                return false;
            }
            if ((model.Origem == "Falha Interna de Equipamento" && model.Componente == "Equipamento Relacionado a Falha"))
            {
                ModelState.AddModelError("Componente", "O Componente da Falha não Pode Estar Vazio");
                return false;
            }
            if (string.IsNullOrEmpty(model.Tipo))
            {
                ModelState.AddModelError("Tipo", "O Tipo da Falha não Deve Estar Vazio");
                return false;
            }
            return true;
        }


        public IActionResult FailIndex()
        {
            string cnpj = HttpContext.Session.GetString("cnpj");
            List <FailModel> list = failService.ListFails(cnpj);
            return View(list);
        }

        public IActionResult CreateFail()
        {   
            FailModel model = new FailModel();
            model.Cnpj = HttpContext.Session.GetString("cnpj");
            ViewBag.Equips = equipService.ListEquipSelect(HttpContext.Session.GetString("cnpj"));
            return View(model);
        }

        public IActionResult EditFail(int id)
        {
            FailModel model = new FailModel();
            return View();
        }

        public IActionResult DeleteFail(int id)
        {
            failService.Delete(id);
            return RedirectToAction("FailIndex");
        }

        public IActionResult InsertFail(FailModel model, string operation)
        {
            try
            {
                if (!ValidateFail(model, operation))
                {
                    ViewBag.Equips = equipService.ListEquipSelect(HttpContext.Session.GetString("cnpj"));
                    ViewBag.Mode = operation;
                    return View("CreateFail", model);
                }
                else
                {
                    ViewBag.Ok = "ok";
                    if (operation != "U")
                    {
                        failService.Insert(model);
                    }
                    else
                    {
                        failService.Edit(model);
                    }
                    return RedirectToAction("FailIndex");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        #endregion Fail

 

        public IActionResult AccountIndex()
        {
            return View();
        }
    }
}