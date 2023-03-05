using APIBloc.Models;
using MySql.Data.MySqlClient;

namespace APIBloc.Services
{
    public static class EmployeeService
    {
        private static string TableName = "Employee";

        public static bool GetAll(out List<Employee> result)
        {
            result = new List<Employee>();
            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                string sql = @$"SELECT T1.*, S.City, Ser.Name FROM `{TableName}` T1
                                INNER JOIN `Site` S ON T1.IDSite = S.IDSite
                                INNER JOIN `Service` Ser ON T1.IDService = Ser.IDService
                                ORDER BY T1.Lastname";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                while (reader.Read())
                {
                    result.Add(new Employee()
                    {
                        IDEmployee = reader.GetInt32("IDEmployee"),
                        Firstname = reader.GetString("Firstname"),
                        Lastname = reader.GetString("Lastname"),
                        HomePhone = reader.GetString("HomePhone"),
                        MobilePhone = reader.GetString("MobilePhone"),
                        Email = reader.GetString("Email"),
                        IDSite = reader.GetInt32("IDSite"),
                        IDService = reader.GetInt32("IDService"),
                        Site = new Site()
                        {
                            IDSite = reader.GetInt32("IDSite"),
                            City = reader.GetString("City")
                        },
                        Service = new Service()
                        {
                            IDService = reader.GetInt32("IDService"),
                            Name = reader.GetString("Name")
                        }
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Get(int id, out Employee? result)
        {
            result = null;
            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                string sql = @$"SELECT T1.*, S.City, Ser.Name FROM `{TableName}` T1
                                INNER JOIN `Site` S ON T1.IDSite = S.IDSite
                                INNER JOIN `Service` Ser ON T1.IDService = Ser.IDService
                                WHERE IDEmployee = {id}
                                ORDER BY T1.Lastname";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                if (reader.Read())
                {
                    result = new Employee()
                    {
                        IDEmployee = reader.GetInt32("IDEmployee"),
                        Firstname = reader.GetString("Firstname"),
                        Lastname = reader.GetString("Lastname"),
                        HomePhone = reader.GetString("HomePhone"),
                        MobilePhone = reader.GetString("MobilePhone"),
                        Email = reader.GetString("Email"),
                        IDSite = reader.GetInt32("IDSite"),
                        IDService = reader.GetInt32("IDService"),
                        Site = new Site()
                        {
                            IDSite = reader.GetInt32("IDSite"),
                            City = reader.GetString("City")
                        },
                        Service = new Service()
                        {
                            IDService = reader.GetInt32("IDService"),
                            Name = reader.GetString("Name")
                        }
                    };
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool GetFiltered(string? firstname, string? lastname, int? idService, int? idSite, out List<Employee> result)
        {
            result = new List<Employee>();

            if (!DBConnection.ConnectToDB())
            {
                return false;
            }

            try
            {
                List<string> filter = new List<string>();

                if (firstname != null)
                {
                    filter.Add($"T1.Firstname LIKE '{firstname}%'");
                }

                if (lastname != null)
                {
                    filter.Add($"T1.Lastname LIKE '{lastname}%'");
                }

                if (idService != null)
                {
                    filter.Add($"T1.IDService = {idService}");
                }

                if (idSite != null)
                {
                    filter.Add($"T1.IDSite = {idSite}");
                }

                string whereClause = "";
                if (filter.Count > 0)
                {
                    whereClause = "WHERE " + string.Join(" AND ", filter);
                }

                string sql = @$"SELECT T1.*, S.City, Ser.Name FROM `{TableName}` T1
                                INNER JOIN `Site` S ON T1.IDSite = S.IDSite
                                INNER JOIN `Service` Ser ON T1.IDService = Ser.IDService
                                {whereClause}";

                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                while (reader.Read())
                {
                    result.Add(new Employee()
                    {
                        IDEmployee = reader.GetInt32("IDEmployee"),
                        Firstname = reader.GetString("Firstname"),
                        Lastname = reader.GetString("Lastname"),
                        HomePhone = reader.GetString("HomePhone"),
                        MobilePhone = reader.GetString("MobilePhone"),
                        Email = reader.GetString("Email"),
                        IDSite = reader.GetInt32("IDSite"),
                        IDService = reader.GetInt32("IDService"),
                        Site = new Site()
                        {
                            IDSite = reader.GetInt32("IDSite"),
                            City = reader.GetString("City")
                        },
                        Service = new Service()
                        {
                            IDService = reader.GetInt32("IDService"),
                            Name = reader.GetString("Name")
                        }
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Post(Employee data, out int insertedId)
        {
            try
            {
                MySqlCommand command = SqlUtils.CreateInsertCommand(TableName,
                    "Firstname", data.Firstname,
                    "Lastname", data.Lastname,
                    "HomePhone", data.HomePhone,
                    "MobilePhone", data.MobilePhone,
                    "Email", data.Email,
                    "IDSite", data.IDSite,
                    "IDService", data.IDService);

                return command.ExecuteNonQueryToDB(out insertedId);
            }
            catch (Exception)
            {
                insertedId = -1;
                return false;
            }
        }

        public static bool Update(int id, Employee data)
        {
            try
            {
                return DBConnection.ExecuteUpdateCommandToDB(TableName, id,
                    "Firstname", data.Firstname,
                    "Lastname", data.Lastname,
                    "HomePhone", data.HomePhone,
                    "MobilePhone", data.MobilePhone,
                    "Email", data.Email,
                    "IDSite", data.IDSite,
                    "IDService", data.IDService);
            }
            catch (Exception)
            {
                return false;
            }
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
