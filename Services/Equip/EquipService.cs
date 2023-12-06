using IndigoErp.DAO.Value_Entities;
using IndigoErp.Models;
using System.Data;

namespace IndigoErp.Services
{
    public class EquipService
    {
        private ValidationService val = new ValidationService();
        private EquipDAO dao = new EquipDAO();
        public string ValidateEquip(EquipModel model,string operation)
        {

            if (!val.ValidateString(model.Marca) || 
                !val.ValidateString(model.Modelo) ||
                !val.ValidateString(model.Nome) ||
                !val.ValidateString(model.NumeroSerie)) 
            {
                return "stringError";
            }
            else if ( model.Setor == "Setor")
            {
                return "sectionNotChosed";
            }
            else if (dao.SearchSimilar(model.NumeroSerie) && operation != "U")
            {
                return "similarFound";
            }
            else
            {
                return "ok";
            }
        }

        public EquipModel GetEquip(int id) 
        {
          
            EquipModel model = dao.SearchEquip(id);
        
            return model;
        }

        public void Delete(int id)
        {
            dao.Delete("Equipamento",id);
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

        public List<EquipModel> ListEquip(string column, string filter,string order)
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
