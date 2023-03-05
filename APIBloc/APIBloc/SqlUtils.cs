using MySql.Data.MySqlClient;
using System.Reflection;

namespace APIBloc
{
    public static class SqlUtils
    {
        #region SQL Commands Creation

        public static MySqlCommand CreateInsertCommand(string tableName, params object?[] fieldsAndValues)
        {
            if (fieldsAndValues.Length % 2 != 0)
            {
                throw new ArgumentException("Nombre de paramètres invalides !");
            }

            MySqlCommand cmd = new MySqlCommand();
            List<string> fieldsList = new List<string>();

            for (int i = 0; i < fieldsAndValues.Length; i += 2)
            {
                if (fieldsAndValues[i] is not string field)
                {
                    throw new ArgumentException("Le nom du champ n'est pas de type string !");
                }
                object? value = fieldsAndValues[i + 1];

                fieldsList.Add(field);
                cmd.Parameters.AddWithValue("@" + field, value);
            }

            string fields = string.Join(", ", fieldsList.Select(f => $"`{f}`"));
            string parameters = string.Join(", ", fieldsList.Select(f => $"@{f}"));

            cmd.CommandText = $"INSERT INTO `{tableName}` ({fields}) VALUES ({parameters})";

            return cmd;
        }

        public static MySqlCommand CreateUpdateCommand(string tableName, int id, params object?[] fieldsAndValues)
        {
            return CreateUpdateCommand(tableName, "ID" + tableName, id, fieldsAndValues);
        }

        public static MySqlCommand CreateUpdateCommand(string tableName, string idFieldName, int id, params object?[] fieldsAndValues)
        {
            if (fieldsAndValues.Length % 2 != 0)
            {
                throw new ArgumentException("Nombre de paramètres invalides !");
            }

            MySqlCommand cmd = new MySqlCommand();
            List<string> setPartList = new List<string>();

            for (int i = 0; i < fieldsAndValues.Length; i += 2)
            {
                if (fieldsAndValues[i] is not string field)
                {
                    throw new ArgumentException("Le nom du champ n'est pas de type string !");
                }

                object? value = fieldsAndValues[i + 1];

                setPartList.Add($"`{field}` = @{field}");
                cmd.Parameters.AddWithValue("@" + field, value);
            }

            string setPart = string.Join(", ", setPartList);

            cmd.CommandText = $"UPDATE `{tableName}` SET {setPart} WHERE `{idFieldName}` = {id}";

            return cmd;
        }

        public static MySqlCommand CreateDeleteMethod(string tableName, int id)
        {
            return CreateDeleteMethod(tableName, "ID" + tableName, id);
        }

        public static MySqlCommand CreateDeleteMethod(string tableName, string idFieldName, int id)
        {
            string sql = $"DELETE FROM `{tableName}` WHERE `{idFieldName}` = {id}";

            return new MySqlCommand(sql);
        }

        #endregion

    }
}
