using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerData
{
    public class DCategory
    {
        private int idCategory;
        private string name;
        private string description;
        private string textSearch;

        public DCategory()
        {

        }

        public DCategory(int idCategory, string name, string description, string textsearch)
        {
            this.idCategory = idCategory;
            this.name = name;
            this.description = description;
            this.textSearch = textsearch;
        }

        public int IdCategory { get => idCategory; set => idCategory = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string TextSearch { get => textSearch; set => textSearch = value; }

        public string Insert(DCategory category)
        {
            String resp = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Connection.ConnectionDB();
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "spinsert_category";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlParameterId = new SqlParameter();
                sqlParameterId.ParameterName = "@idCategory";
                sqlParameterId.SqlDbType = SqlDbType.Int;
                sqlParameterId.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameterId);

                SqlParameter sqlParameterName = new SqlParameter();
                sqlParameterName.ParameterName = "@name";
                sqlParameterName.SqlDbType = SqlDbType.VarChar;
                sqlParameterName.Size = 50;
                sqlParameterName.Value = category.Name;
                sqlCommand.Parameters.Add(sqlParameterName);

                SqlParameter sqlParameterDescription = new SqlParameter();
                sqlParameterDescription.ParameterName = "@description";
                sqlParameterDescription.SqlDbType = SqlDbType.VarChar;
                sqlParameterDescription.Size = 50;
                sqlParameterDescription.Value = category.description;
                sqlCommand.Parameters.Add(sqlParameterDescription);

                resp = sqlCommand.ExecuteNonQuery() == 1 ? "OK": "Record not entered";
            }
            catch (Exception ex)
            {
                resp = "Erro while inserting"+ ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return resp;
        }

        public string Edit(DCategory category)
        {
            String resp = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Connection.ConnectionDB();
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "spedit_category";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlParameterId = new SqlParameter();
                sqlParameterId.ParameterName = "@idCategory";
                sqlParameterId.SqlDbType = SqlDbType.Int;
                sqlParameterId.Value = category.idCategory;
                sqlCommand.Parameters.Add(sqlParameterId);

                SqlParameter sqlParameterName = new SqlParameter();
                sqlParameterName.ParameterName = "@name";
                sqlParameterName.SqlDbType = SqlDbType.VarChar;
                sqlParameterName.Size = 50;
                sqlParameterName.Value = category.Name;
                sqlCommand.Parameters.Add(sqlParameterName);

                SqlParameter sqlParameterDescription = new SqlParameter();
                sqlParameterDescription.ParameterName = "@description";
                sqlParameterDescription.SqlDbType = SqlDbType.VarChar;
                sqlParameterDescription.Size = 50;
                sqlParameterDescription.Value = category.description;
                sqlCommand.Parameters.Add(sqlParameterDescription);

                resp = sqlCommand.ExecuteNonQuery() == 1 ? "OK" : "Editing was not completed";
            }
            catch (Exception ex)
            {
                resp = "Erro while edited" + ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return resp;
        }

        public string Delete(DCategory category)
        {
            String resp = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Connection.ConnectionDB();
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "spdelete_category";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlParameterId = new SqlParameter();
                sqlParameterId.ParameterName = "@idCategory";
                sqlParameterId.SqlDbType = SqlDbType.Int;
                sqlParameterId.Value = category.idCategory;
                sqlCommand.Parameters.Add(sqlParameterId);

                resp = sqlCommand.ExecuteNonQuery() == 1 ? "OK" : "Deleted was not completed";
            }
            catch (Exception ex)
            {
                resp = "Erro while deleted" + ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return resp;
        }

        public DataTable Show()
        {
            DataTable DTResult = new DataTable("Category");
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = Connection.ConnectionDB();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "spshow_category";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(DTResult);
            }catch(Exception ex)
            {
                DTResult = null;
            }

            return DTResult;
        }

        public DataTable SearchName(DCategory category)
        {
            DataTable DTResult = new DataTable("Category");
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = Connection.ConnectionDB();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "spsearch_nameCategory";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                

                SqlParameter sqlParameterSearch = new SqlParameter();
                sqlParameterSearch.ParameterName = "@textsearch";
                sqlParameterSearch.SqlDbType = SqlDbType.VarChar;
                sqlParameterSearch.Size = 50;
                sqlParameterSearch.Value = category.textSearch;
                sqlCommand.Parameters.Add(sqlParameterSearch);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(DTResult);
            }
            catch (Exception ex)
            {
                DTResult = null;
            }

            return DTResult;
        }
    }
}
