using System.Text.Json.Serialization;

namespace ClientLourdBloc.API.Models
{
    public class Site
    {
        [JsonPropertyName("idSite")]
        public int IDSite { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
    }
}
