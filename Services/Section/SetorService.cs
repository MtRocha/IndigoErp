using IndigoErp.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IndigoErp.Services
{
    
    public class SetorService
    {
        private SetorDAO dao = new SetorDAO();

        public List<SelectListItem> ListSections(string cnpj)
        {
            List<string> sections = dao.ListSections(cnpj);
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (string section in sections){

                listItems.Add(new SelectListItem(section, section));
            
            }
            return listItems;
        }
    }
}
