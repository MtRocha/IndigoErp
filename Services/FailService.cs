using Microsoft.AspNetCore.Mvc.Rendering;
using IndigoErp.DAO;
using IndigoErp.Models;

namespace IndigoErp.Services
{
    public class FailService
    {
        private FailDAO dao = new FailDAO();

        public FailModel GetReport(int id)
        {
            FailModel model = dao.ConsultaPorId(id);

            return model;
        }

        public void Delete(int id)
        {
            dao.Excluir( id);
        }

        public string Insert(FailModel model)
        {
            try
            {
                dao.Inserir(model);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string Edit(FailModel model)
        {
            try
            {
                dao.AlterarFalha(model, model.Id);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<FailModel> ListFails(string cnpj)
        {
            List<FailModel> fails = dao.Consulta(cnpj);

            return fails;
        }

        public List<SelectListItem> ListFailsAsync(string cnpj)
        {
            List<FailModel> fails = dao.Consulta(cnpj);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem("Causa", "Causa"));

            foreach (FailModel fail in fails)
            {
                listItems.Add(new SelectListItem(fail.Tipo, fail.Tipo));
            }
            return listItems;
        }
    }
}