using APIBloc.Models;
using MySql.Data.MySqlClient;

namespace APIBloc.Services
{
    public static class SiteService
    {
        private static string TableName = "Site";

        public static bool GetAll(out List<Site> result)
        {
            result = new List<Site>();
            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                string sql = $"SELECT * FROM `{TableName}`";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                while (reader.Read())
                {
                    result.Add(new Site()
                    {
                        IDSite = reader.GetInt32("IDSite"),
                        City = reader.GetString("City")
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Get(int id, out Site? result)
        {
            result = null;
            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                string sql = $"SELECT * FROM `{TableName}` WHERE `ID{TableName}` = {id}";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                if (reader.Read())
                {
                    result = new Site()
                    {
                        IDSite = reader.GetInt32("IDSite"),
                        City = reader.GetString("City")
                    };
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Post(string data, out int insertedId)
        {
            try
            {
                MySqlCommand command = SqlUtils.CreateInsertCommand(TableName,
                    "City", data);

                return command.ExecuteNonQueryToDB(out insertedId);
            }
            catch (Exception)
            {
                insertedId = -1;
                return false;
            }
        }

        public static bool Update(int id, string data)
        {
            try
            {
                return DBConnection.ExecuteUpdateCommandToDB(TableName, id,
                    "City", data);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool GetDeletionPossibility(int id, out bool canDelete)
        {
            canDelete = false;
            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                string sql = $"SELECT COUNT(1) Count FROM `Employee` WHERE `IDSite` = {id}";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                if (reader.Read())
                {
                    int count = reader.GetInt32("Count");
                    if (count > 0)
                    {
                        canDelete = false;
                    }
                    else
                    {
                        canDelete = true;
                    }
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Delete(int id)
        {
            try
            {
                return DBConnection.ExecuteDeleteCommandToDB(TableName, id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
