using IndigoErp.Models;
using IndigoErp.DAO;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDAO dao = new EmployeeDAO();
        public bool ValidaFuncionario(EmployeeModel model, string Operacao, int IdAntigo)
        {
            if (string.IsNullOrEmpty(model.Nome))
            {
                ModelState.AddModelError("Nome", "Insira um Nome");
                return false;
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                ModelState.AddModelError("Login", "Insira um Login");
                return false;
            }
            if (string.IsNullOrEmpty(model.Senha))
            {
                ModelState.AddModelError("Senha", "Insira uma Senha");
                return false;
            }
            if (model.Funcao == "0")
            {
                ModelState.AddModelError("Funcao", "Selecione uma Função");
                return false;
            }
            if (model.Setor == "Setor")
            {
                ModelState.AddModelError("Setor", "Selecione um Setor");
                return false;
            }

            return true;

        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            List<EmployeeModel> falhas = dao.Consulta(HttpContext.Session.GetString("cnpj"));
            return View(falhas);
        }

        public IActionResult CreateEmployee()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            SetorService service = new SetorService();
            ViewBag.Sections = service.ListSections(HttpContext.Session.GetString("cnpj"));
            EmployeeModel model = new EmployeeModel();
            return View(model);
        }

        public virtual IActionResult InserirFuncionario(EmployeeModel model, string Operacao, int IdAntigo)
        {
            try
            {
                if (!ValidaFuncionario(model, Operacao, IdAntigo))
                {
                    SetorService service = new SetorService();
                    ViewBag.Sections = service.ListSections(HttpContext.Session.GetString("cnpj"));
                    ViewBag.Operacao = Operacao;
                    ViewBag.IdAntigo = IdAntigo;
                    return View("CreateEmployee", model);
                }
                else
                {
                    EmployeeDAO dao = new EmployeeDAO();
                    model.Cnpj = HttpContext.Session.GetString("cnpj");
                    if (Operacao != "U")
                    {
                        dao.Inserir(model);
                    }
                    else
                    {
                        dao.Alterar(model);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        public virtual IActionResult DeleteEmployee(int id)
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();

                dao.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        public IActionResult EditEmployee(int id)
        {
            try

            {
                ViewBag.Name = HttpContext.Session.GetString("nome");
                ViewBag.Operacao = "U";
                SetorService service = new SetorService();
                ViewBag.Sections = service.ListSections(HttpContext.Session.GetString("cnpj"));
                EmployeeDAO dao = new EmployeeDAO();
                EmployeeModel falha = dao.ConsultaPorId(id);
                if (falha == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("CreateEmployee", falha);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel());
            }

        }
    }
}
