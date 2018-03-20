using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerData
{
    public class Connection
    {
        public static SqlConnection ConnectionDB()
        {
            SqlConnection connection = new SqlConnection(@"data source=VINÍCIUSRODRIGU\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=dbEcommerce");
            return connection;
        }
    }
}
