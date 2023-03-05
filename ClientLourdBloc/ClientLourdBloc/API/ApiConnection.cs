using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClientLourdBloc.API
{
    public static class ApiConnection
    {
        private static string uri = "https://localhost:7128/";
        public static HttpClient Client { get; }

        static ApiConnection()
        {
            HttpClient newClient = new HttpClient();
            newClient.BaseAddress = new Uri(uri);
            newClient.DefaultRequestHeaders.Accept.Clear();
            newClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Client = newClient;
        }
    }
}
