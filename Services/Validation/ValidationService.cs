using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics;
using IndigoErp.Models;
using IndigoErp.DAO.Value_Entities;
using IndigoErp.DAO;

namespace IndigoErp.Services
{
    public class ValidationService : Controller
    {
        public bool ValidateString(string value)
        {
            Regex regex = new Regex("^[\"\';:]$");

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            if (regex.IsMatch(value)) 
            { 
                return false;
            }
            else { return true; }
        
        }

        public bool ValidateInt(int value,bool isLowerThanZero,bool isEqualToZero)
        {
        
            if ((value == 0 && !isEqualToZero) || (value < 0 && !isLowerThanZero)) 
            {
                return true;
            }
            else{ return false; }
        
        }

        public void ValidateEquip(EquipModel model, string operation)
        {
            EquipDAO dao = new EquipDAO();
            ModelState.Clear();
            if (ValidateString(model.Marca)) 
            {
                ModelState.AddModelError("Marca", "Caractéres Inválidos");
            }
            if(ValidateString(model.Modelo))
            {
                ModelState.AddModelError("Modelo", "Caractéres Inválidos");
            }
            if(ValidateString(model.Nome))
            {
                ModelState.AddModelError("Nome", "Caractéres Inválidos");
            }
            if(ValidateString(model.NumeroSerie))
            {
                ModelState.AddModelError("NumeroSerie", "Caractéres Inválidos");
            }
            if(model.Setor == "Setor")
            {
                ModelState.AddModelError("Setor", "Escolha Um setor Para o Equipamento");
            }
            if (dao.SearchSimilar(model.NumeroSerie) && operation != "U")
            {
                ModelState.AddModelError("Id", "Equipamento já Existe");
            }
        }

        public void ValidaReport(ReportModel report, string Operation, string HasMaintence)
        {

            ReportDAO dao = new ReportDAO();

            if (report.MaintenceTYpe == "Tipo de Manutenção" && HasMaintence == "sim")
            {
                ModelState.AddModelError("NumeroTeste", "Informe o Numero do Teste");
            }
            if (report.Section == "Setor")
            {
                ModelState.AddModelError("Celula", "Selecione uma Célula");
            }
            if (report.Origin != "INTERNA" && report.Origin != "EXTERNA")
            {
                ModelState.AddModelError("Origem", "Selecione uma Origem");
            }
            if (report.FailType == "Componente")
            {
                ModelState.AddModelError("Componente", "Selecione um Componente");
            }
            if (report.Origin == "INTERNA" && report.FailCause == "Causa")
            {
                ModelState.AddModelError("Tipo", "Selecione uma Causa");
            }
            if (report.Begin > DateTime.Now && report.InitialDate == DateTime.Today || report.Begin == null)
            {
                ModelState.AddModelError("Inicio", "Horário Inválido");
            }
            if (Operation != "I" && report.End == null)
            {
                ModelState.AddModelError("Final", "Digite um Horário");
            }
            if (report.FailType == "OUTRO" && string.IsNullOrEmpty(report.Description))
            {
                ModelState.AddModelError("Descrição", "Adicione Alguma Descrição");
            }
            if (report.InitialDate > DateTime.Now || report.InitialDate == null || report.InitialDate == DateTime.MinValue)
            {
                ModelState.AddModelError("Data", " Data Inválida");
            }
            if (report.Begin > DateTime.Now && report.InitialDate == DateTime.Today || Convert.ToDateTime(report.Begin).Hour == 0)
            {
                ModelState.AddModelError("Inicio", "Horário Inválido");
            }
            if ((report.FinalDate < report.InitialDate || report.FinalDate > DateTime.Today) || report.FinalDate == null && Operation != "I" || report.FinalDate == DateTime.MinValue)
            {
                ModelState.AddModelError("DataFinal", " Data Inválida");
            }
            if ((report.End < report.Begin && report.InitialDate == DateTime.Today && report.FinalDate == DateTime.Today))
            {
                ModelState.AddModelError("Final", "Horário Inválido");
            }
            if (report.End > DateTime.Now && report.FinalDate == DateTime.Today || (report.FinalDate == null || Convert.ToDateTime(report.End).Hour == 0) && Operation != "I")
            {
                ModelState.AddModelError("Final", "Horário Inválido");
            }
        }

    }
}
