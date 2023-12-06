using IndigoErp.DAO;
using IndigoErp.Services;
using IndigoErp.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class OcorrenciasController : Controller
    {
        ReportService service = new ReportService();

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

                if (!ModelState.IsValid)
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

