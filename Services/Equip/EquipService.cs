using IndigoErp.DAO.Value_Entities;
using IndigoErp.Models;
using System.Data;

namespace IndigoErp.Services
{
    public class EquipService
    {
        private ValidationService val = new ValidationService();
        private EquipDAO dao = new EquipDAO();
        public string ValidateEquip(EquipModel model)
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
            else if (dao.SearchSimilar(model.NumeroSerie))
            {
                return "similarFound";
            }
            else
            {
                return "ok";
            }
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

        public List<EquipModel> EquipQuery(string column, string filter,string order)
        {

            DataTable table = new DataTable();

            if (!string.IsNullOrEmpty(filter))
            {

                 table = dao.Listing("EQUIPAMENTO", column);

            }
            else
            {

                 table = dao.Listing("EQUIPAMENTO", column, filter, order);

            }      
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
