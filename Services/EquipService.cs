using IndigoErp.DAO;
using IndigoErp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

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

        public List<SelectListItem> ListEquipSelect(string cnpj)
        {

            DataTable table = new DataTable();
            QueryModel query = new QueryModel("EQUIPAMENTO","CNPJ_DOMINIO",cnpj);
            table = dao.Query(query);
            List<EquipModel> equipList = new List<EquipModel>();
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem($"Equipamento", $"Equipamento"));

            if (table != null)
            {
                foreach (DataRow item in table.Rows)
                {
                    equipList.Add(dao.CreateObject(item));
                }

                foreach (EquipModel item in equipList)
                {
                    itemList.Add(new SelectListItem($"{item.Nome} {item.Modelo} {item.NumeroSerie}", $"{item.Nome} {item.Modelo} {item.NumeroSerie}"));
                }

                return itemList;
            }
            else
            {
                return null;
            }
        }

        public List<string> ListEquipBySection(string section)
        {

            DataTable table = new DataTable();
            QueryModel query = new QueryModel("EQUIPAMENTO", "SETOR", section);
            table = dao.Query(query);
            List<EquipModel> equipList = new List<EquipModel>();
            List<string> itemList = new List<string>();

            if (table != null)
            {
                foreach (DataRow item in table.Rows)
                {
                    equipList.Add(dao.CreateObject(item));
                }

                foreach (EquipModel item in equipList)
                {
                    itemList.Add($"{item.Nome} {item.Modelo} {item.NumeroSerie}");
                }

                return itemList;
            }
            else
            {
                return null;
            }
        }
    }
}