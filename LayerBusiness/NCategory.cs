using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerBusiness;
using System.Data;
using LayerData;

namespace LayerBusiness
{
    public class NCategory
    {
        public static string Insert(string name, string description)
        {
            DCategory obj = new LayerData.DCategory();
            obj.Name = name;
            obj.Description = description;
            return obj.Insert(obj);
        }

        public static string Edit(int idCategory, string name, string description)
        {
            DCategory obj = new LayerData.DCategory();
            obj.Name = name;
            obj.IdCategory = idCategory;
            obj.Description = description;
            return obj.Edit(obj);
        }

        public static string Delete(int idCategory)
        {
            DCategory obj = new LayerData.DCategory();
            obj.IdCategory = idCategory;
            return obj.Delete(obj);
        }

        public static DataTable Show()
        {
            return new DCategory().Show();
        }

        public static DataTable SearchName(string searchName)
        {
            DCategory obj = new DCategory();
            obj.TextSearch = searchName;

            return obj.SearchName(obj);
        }
    }
}
