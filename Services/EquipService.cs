using IndigoErp.DAO;
using IndigoErp.Models;
using System.Data;

namespace IndigoErp.Services
{
    public class EquipService
    {
        private EquipDAO dao = new EquipDAO();

        public EquipModel GetEquip(int id)
        {
            EquipModel model = dao.SearchEquip(id);

            return model;
        }

        public void Delete(int id)
        {
            dao.Delete("Equipamento", id);
        }

        public string Insert(EquipModel model)
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

        public string Edit(EquipModel model)
        {
            try
            {
                dao.Update(model);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<EquipModel> ListEquip(string column, string filter, string order)
        {
            DataTable table = new DataTable();

            QueryModel query = string.IsNullOrEmpty(filter) ? new QueryModel("EQUIPAMENTO", column) : new QueryModel("EQUIPAMENTO", column, filter, order);

            table = dao.Listing(query);

            List<EquipModel> list = new List<EquipModel>();

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