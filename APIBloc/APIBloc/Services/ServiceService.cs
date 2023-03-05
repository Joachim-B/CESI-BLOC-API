using APIBloc.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace APIBloc.Services
{
    public static class ServiceService
    {
        private static string TableName = "Service";

        public static bool GetAll(out List<Service> result)
        {
            result = new List<Service>();
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
                    result.Add(new Service()
                    {
                        IDService = reader.GetInt32("IDService"),
                        Name = reader.GetString("Name")
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Get(int id, out Service? result)
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
                    result = new Service()
                    {
                        IDService = reader.GetInt32("IDService"),
                        Name = reader.GetString("Name")
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
                    "Name", data);

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
                    "Name", data);
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
                string sql = $"SELECT COUNT(1) Count FROM `Employee` WHERE `IDService` = {id}";

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
            catch (Exception)
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
