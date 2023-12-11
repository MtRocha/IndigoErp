using IndigoErp.DAO;
using IndigoErp.Models;
using System.Data;

namespace IndigoErp.Services
{
    public class ReportService
    {
        private ReportDAO dao = new ReportDAO();

        public ReportModel GetReport(int id)
        {
            ReportModel model = dao.SearchReport(id);

            return model;
        }

        public void Delete(int id)
        {
            dao.Delete("Reportamento", id);
        }

        public string Insert(ReportModel model)
        {
            try
            {
                dao.Insert(model);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string Edit(ReportModel model)
        {
            try
            {
                dao.Update(model, model.Id);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<ReportModel> ListReport(string column, string filter, string order)
        {
            DataTable table = new DataTable();

            QueryModel query = string.IsNullOrEmpty(filter) ? new QueryModel("REPORTS", column) : new QueryModel("REPORTS", column, filter, order);

            table = dao.Listing(query);

            List<ReportModel> list = new List<ReportModel>();

            if (table != null)
            {
                foreach (DataRow item in table.Rows)
                {
                    list.Add(dao.CreateObject(item));
                }

                return list;
            }
            else
            {
                return null;
            }
        }
    }
}