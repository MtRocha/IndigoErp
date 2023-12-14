using IndigoErp.DAO;
using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class OcorrenciasController : Controller
    {
        private ReportService service = new ReportService();
        private EquipService equipService = new EquipService();
        private SetorService setorService = new SetorService();
        public bool ValidaReport(ReportModel report, string Operation, string HasMaintence)
        {
            ReportDAO dao = new ReportDAO();

            if (report.MaintenceTYpe == "Tipo de Manutenção" && HasMaintence == "sim")
            {
                ModelState.AddModelError("NumeroTeste", "Informe o Numero do Teste");
                return false;
            }
            if (report.Section == "Setor")
            {
                ModelState.AddModelError("Celula", "Selecione uma Célula");
                return false;
            }
            if (report.Origin != "Falha Interna de Equipamento" && report.Origin != "Falha Externa da Operação" && report.Origin != "FALHA INTERNA DE EQUIPAMNETO" && report.Origin != "FALHA EXTERNA DA OPERAÇÃO")
            {
                ModelState.AddModelError("Origem", "Selecione uma Origem");
                return false;
            }
            if (report.FailType == "Componente")
            {
                ModelState.AddModelError("Componente", "Selecione um Componente");
                return false;
            }
            if (report.Origin == "INTERNA" && report.FailCause == "Causa")
            {
                ModelState.AddModelError("Tipo", "Selecione uma Causa");
                return false;
            }
            if (report.Begin > DateTime.Now && report.InitialDate == DateTime.Today || report.Begin == null)
            {
                ModelState.AddModelError("Inicio", "Horário Inválido");
                return false;
            }
            if (Operation != null && report.End == null)
            {
                ModelState.AddModelError("Final", "Digite um Horário");
                return false;
            }
            if (report.FailType == "OUTRO" && string.IsNullOrEmpty(report.Description))
            {
                ModelState.AddModelError("Descrição", "Adicione Alguma Descrição");
                return false;
            }
            if (report.InitialDate > DateTime.Now || report.InitialDate == null || report.InitialDate == DateTime.MinValue)
            {
                ModelState.AddModelError("Data", " Data Inválida");
                return false;
            }
            if (report.Begin > DateTime.Now && report.InitialDate == DateTime.Today || Convert.ToDateTime(report.Begin).Hour == 0)
            {
                ModelState.AddModelError("Inicio", "Horário Inválido");
                return false;
            }
            if ((report.FinalDate < report.InitialDate || report.FinalDate > DateTime.Today) || report.FinalDate == null && Operation != null || report.FinalDate == DateTime.MinValue)
            {
                ModelState.AddModelError("DataFinal", " Data Inválida");
                return false;
            }
            if ((report.End < report.Begin && report.InitialDate == DateTime.Today && report.FinalDate == DateTime.Today))
            {
                ModelState.AddModelError("Final", "Horário Inválido");
                return false;
            }
            if (report.End > DateTime.Now && report.FinalDate == DateTime.Today || (report.FinalDate == null || Convert.ToDateTime(report.End).Hour == 0) && Operation != null)
            {
                ModelState.AddModelError("Final", "Horário Inválido");
                return false;
            }
            return true;
        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            ReportDAO dao = new ReportDAO();
            List<ReportModel> list = dao.ConsultaPendente(HttpContext.Session.GetString("cnpj"));
            return View(list);
        }

        public IActionResult ReportHistory()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            ReportDAO dao = new ReportDAO();
            List<ReportModel> list = dao.Consulta(HttpContext.Session.GetString("cnpj"));
            return View("ReportHistory",list);
        }

        public IActionResult CreateReport()
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            ReportModel report = new ReportModel();
            ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
            return View(report);
        }

        public IActionResult EndReport(int id)
        {
            ViewBag.Name = HttpContext.Session.GetString("nome");
            ReportDAO dao = new ReportDAO();
            ReportModel report = dao.SearchReport(id);
            ViewBag.Operacao = "U";
            return View("CreateReport", report);
        }
        public IActionResult InserirReport(ReportModel report, string Operacao, int id, string TesteIncluso, string Finalizacao)
        {
            try
            {
                if (!ValidaReport(report, Operacao, TesteIncluso))
                {
                    ViewBag.Sections = setorService.ListSections(HttpContext.Session.GetString("cnpj"));
                    report.End = null;
                    ViewBag.Titulo = "Operadores";
                    ViewBag.Operacao = Operacao;
                    ViewBag.Id = id;
                    return View("CreateReport", report);
                }
                else
                {
                    if (TesteIncluso == "nao")
                    {
                        report.MaintenceTYpe = "Ocorrência Sem Tipo de Manutenção ";
                    }
                    if (Operacao == null)
                    {
                        ReportDAO dao = new ReportDAO();

                        if (Finalizacao == "nao")
                        {
                            report.Status = "PENDENTE";
                        }
                        else
                        {
                            report.Status = "FINALIZADO";
                        }

                        report.EmployeeId = HttpContext.Session.GetString("nome"); ;
                        report.Cnpj = HttpContext.Session.GetString("cnpj");
                        dao.Insert(report);

                        ViewBag.Operacao = "sucesso";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ReportDAO dao = new ReportDAO();
                        report.EmployeeId = HttpContext.Session.GetString("nome"); ;
                        report.Cnpj = HttpContext.Session.GetString("cnpj");
                        ViewBag.Operacao = "sucesso";
                        report.Status = "FINALIZADO";
                        dao.Update(report, id);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception erro)
            {
                ErrorViewModel errorView = new ErrorViewModel();

                errorView.RequestId = erro.ToString();

                return View("Error", errorView);
            }
        }

        [HttpGet]
        public IActionResult SearchFails(string text)

        {
            try
            {
                FailDAO dao = new FailDAO();

                List<string> lista = text != "Falha Externa da Operação" ? dao.ConsultaCausa(text) : dao.ConsultaCausa("Falha Sem Equipamento");
                List<string> causas = new List<string>();

                foreach (var componente in lista)
                {
                    causas.Add(componente.ToString());
                }

                return Json(new { lista = causas });
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel());
            }


        }

        [HttpGet]
        public IActionResult SearchEquips(string text)

        {
            try
            {
                List<string> lista = equipService.ListEquipBySection(text);

                return Json(new { lista = lista });
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel());
            }


        }

    }
}