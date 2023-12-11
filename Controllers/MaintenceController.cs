using IndigoErp.Models;
using IndigoErp.Services;
using IndigoErp.DAO;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class MaintenceController : Controller
    {
        private FailService failService;

        #region Fail

        public bool ValidateFail(FailModel model, string Operacao)
        {


            ModelState.Clear();
            FailDAO dao = new FailDAO();

            if (Operacao == "A" && dao.ConsultaPorId(model.Id) != null)
            {
                ModelState.AddModelError("Id", "Id Já Esta em Uso");
                return false;
            }
            if (Operacao == "A" && model.Id <= 0)
            {
                ModelState.AddModelError("Id", "Id Inválido");
                return false;
            }
            if (model.Origem != "EXTERNA" && model.Origem != "INTERNA")
            {
                ModelState.AddModelError("Origem", "Selecione uma Origem");
                return false;
            }
            if (string.IsNullOrEmpty(model.Componente))
            {
                ModelState.AddModelError("Componente", "O Componente da Falha não Pode Estar Vazio");
                return false;
            }
            if (model.Origem == "INTERNA" && string.IsNullOrEmpty(model.Tipo))
            {
                ModelState.AddModelError("Tipo", "O Tipo da Falha não Pode Estar Vazio");
                return false;
            }
            if (model.Origem == "EXTERNA" && !string.IsNullOrEmpty(model.Tipo))
            {
                ModelState.AddModelError("Tipo", "O Tipo da Falha Deve Estar Vazio em uma Falha Externa");
                return false;
            }
            return true;
        }


        public IActionResult FailIndex()
        {
            List <FailModel> list = failService.ListFails(HttpContext.Session.GetString("cnpj"));
            return View(list);
        }

        public IActionResult CreateFail()
        {   
            FailModel model = new FailModel();

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
            return RedirectToAction("Index");
        }

        public IActionResult InsertFail(FailModel model, string operation)
        {
            try
            {
                if (!ValidateFail(model, operation))
                {
                    ViewBag.Mode = operation;
                    return View("CreateEquip", model);
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
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        #endregion Fail

   /*     #region Employee

        public IActionResult EmployeeIndex()
        {
            return View();
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        public IActionResult EditEmployee()
        {
            return View();
        }

        public IActionResult DeleteEmployee(int id)
        {
            return RedirectToAction("Index");
        }

        public IActionResult InsertEmployee(EquipModel model, string operation)
        {
            try
            {
                if (!ValidateEquip(model, operation))
                {
                    ViewBag.Mode = operation;
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

        #endregion */

        public IActionResult AccountIndex()
        {
            return View();
        }
    }
}