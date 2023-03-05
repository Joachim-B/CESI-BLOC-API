using ClientLourdBloc.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientLourdBloc.API
{
    public static class APIRequest
    {
        #region Get Requests

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Employee");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Employee>>(json) is IEnumerable<Employee> requestResult)
                    {
                        employees = requestResult.ToList();
                    }
                }
            }).Wait();

            return employees;
        }
        public static List<Employee> GetAllEmployeesFiltered(string filterExpression)
        {
            List<Employee> employees = new List<Employee>();

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync($"Employee/Filtered?{filterExpression}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Employee>>(json) is IEnumerable<Employee> requestResult)
                    {
                        employees = requestResult.ToList();
                    }
                }
            }).Wait();

            return employees;
        }

        public static List<Site> GetAllSites()
        {
            List<Site> list = new List<Site>();

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Site");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Site>>(json) is IEnumerable<Site> requestResult)
                    {
                        list = requestResult.ToList();
                    }
                }
            }).Wait();

            return list;
        }

        public static List<Service> GetAllServices()
        {
            List<Service> list = new List<Service>();

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Service");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Service>>(json) is IEnumerable<Service> requestResult)
                    {
                        list = requestResult.ToList();
                    }
                }
            }).Wait();

            return list;
        }

        #endregion

        #region Post Requests

        public static bool Post(Employee employee)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(employee);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("Employee", content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool Post(Service service)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(service);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("Service", content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool Post(Site site)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(site);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("Site", content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        #endregion

        #region Put Requests

        public static bool Put(Employee employee)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(employee);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("Employee/" + employee.IDEmployee, content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool Put(Service service)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(service);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("Service/" + service.IDService, content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool Put(Site site)
        {
            bool result = false;

            string json = JsonSerializer.Serialize(site);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("Site/" + site.IDSite, content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        #endregion

        #region Delete Requests

        public static bool DeleteEmployee(int id)
        {
            bool result = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.DeleteAsync("Employee/" + id);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool DeleteService(int id)
        {
            bool result = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.DeleteAsync("Service/" + id);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        public static bool DeleteSite(int id)
        {
            bool result = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.DeleteAsync("Site/" + id);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        #endregion

        #region Login Request

        public static bool TryLogin(string password)
        {
            bool successfulLogin = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("/Admin/TryLogin/" + password);

                if (response.IsSuccessStatusCode)
                {
                    successfulLogin = true;
                }

            }).Wait();

            return successfulLogin;
        }

        #endregion
    }
}
