using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySql.Data.MySqlClient;
using System.Data;

namespace APIBloc
{
    public static class DBConnection
    {
        #region Data

        private static MySqlConnection Connection { get; set; } = new();

        #endregion

        #region Constructor

        static DBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "annuaire";
            string username = "root";
            string password = "root";

            Connection = new MySqlConnection($"host = {host}; port = {port}; database = {database}; username = {username}; password = {password}");

            try
            {
                Connection.Open();
                System.Diagnostics.Debug.WriteLine("Connexion réussie !");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Connection Methods

        public static bool ConnectToDB()
        {
            if (!Connection.Ping())
            {
                return false;
            }

            if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        public static MySqlDataReader ExecuteReaderToDB(this string sql)
        {
            return new MySqlCommand(sql, Connection).ExecuteReader();
        }

        public static bool ExecuteNonQueryToDB(this MySqlCommand mySqlCommand, out int insertedId)
        {
            insertedId = -1;
            mySqlCommand.Connection = Connection;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                insertedId = (int)mySqlCommand.LastInsertedId;
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Request Methods

        public static bool ExecuteInsertCommandToDB(string tableName, params object?[] fieldsAndValues)
        {
            if (!ConnectToDB())
            {
                return false;
            }

            return SqlUtils.CreateInsertCommand(tableName, fieldsAndValues).ExecuteNonQueryToDB(out int insertedID);
        }

        public static bool ExecuteUpdateCommandToDB(string tableName, int id, params object?[] fieldsAndValues)
        {
            return ExecuteUpdateCommandToDB(tableName, "ID" + tableName, id, fieldsAndValues);
        }

        public static bool ExecuteUpdateCommandToDB(string tableName, string idFieldName, int id, params object?[] fieldsAndValues)
        {
            if (!ConnectToDB())
            {
                return false;
            }

            return SqlUtils.CreateUpdateCommand(tableName, idFieldName, id, fieldsAndValues).ExecuteNonQueryToDB(out int insertedID);
        }

        public static bool ExecuteDeleteCommandToDB(string tableName, int id)
        {
            return ExecuteDeleteCommandToDB(tableName, "ID" + tableName, id);
        }

        public static bool ExecuteDeleteCommandToDB(string tableName, string idFieldName, int id)
        {
            if (!ConnectToDB())
            {
                return false;
            }

            return SqlUtils.CreateDeleteMethod(tableName, idFieldName, id).ExecuteNonQueryToDB(out int insertedID);
        }

        #endregion
    }
}
