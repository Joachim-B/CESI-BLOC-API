using System.Text.Json.Serialization;

namespace ClientLourdBloc.API.Models
{
    public class Employee
    {
        [JsonPropertyName("idEmployee")]
        public int IDEmployee { get; set; }

        [JsonPropertyName("firstname")]
        public string Firstname { get; set; } = string.Empty;

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; } = string.Empty;

        [JsonPropertyName("homePhone")]
        public string HomePhone { get; set; } = string.Empty;

        [JsonPropertyName("mobilePhone")]
        public string MobilePhone { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("idSite")]
        public int IDSite { get; set; }

        [JsonPropertyName("idService")]
        public int IDService { get; set; }

        [JsonPropertyName("site")]
        public Site Site { get; set; }

        [JsonPropertyName("service")]
        public Service Service { get; set; }
    }
}
