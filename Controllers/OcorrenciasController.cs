using IndigoErp.DAO;
using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class OcorrenciasController : Controller
    {
        private ReportService service = new ReportService();

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
            if (report.Origin != "INTERNA" && report.Origin != "EXTERNA")
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
            if (Operation != "I" && report.End == null)
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
            if ((report.FinalDate < report.InitialDate || report.FinalDate > DateTime.Today) || report.FinalDate == null && Operation != "I" || report.FinalDate == DateTime.MinValue)
            {
                ModelState.AddModelError("DataFinal", " Data Inválida");
                return false;
            }
            if ((report.End < report.Begin && report.InitialDate == DateTime.Today && report.FinalDate == DateTime.Today))
            {
                ModelState.AddModelError("Final", "Horário Inválido");
                return false;
            }
            if (report.End > DateTime.Now && report.FinalDate == DateTime.Today || (report.FinalDate == null || Convert.ToDateTime(report.End).Hour == 0) && Operation != "I")
            {
                ModelState.AddModelError("Final", "Horário Inválido");
                return false;
            }
            return true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReport()
        {
            return View();
        }

        public IActionResult InserirReport(ReportModel report, string Operacao, int id, string TesteIncluso, string Finalizacao)
        {
            try
            {
                if (!ValidaReport(report, Operacao, TesteIncluso))
                {
                    ModelState.AddModelError("Origem", "Selecione uma Origem");
                    report.End = null;
                    ViewBag.Titulo = "Operadores";
                    ViewBag.Operacao = Operacao;
                    ViewBag.Id = id;
                    return View("FrmReport", report);
                }
                else
                {
                    if (TesteIncluso == "nao")
                    {
                        report.MaintenceTYpe = "Ocorrência Sem Tipo de Manutenção ";
                    }
                    if (ViewBag.Operacao == "I")
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

                        report.EmployeeId = 0000;

                        dao.Inserir(report);

                        ViewBag.Operacao = "sucesso";
                        return RedirectToAction("PgReports");
                    }
                    else
                    {
                        ReportDAO dao = new ReportDAO();

                        ViewBag.Operacao = "sucesso";
                        report.Status = "FINALIZADO";
                        dao.AlterarReport(report, id);
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
    }
}