namespace APIBloc.Models
{
    public class Employee
    {
        public int IDEmployee { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int IDSite { get; set; }
        public int IDService { get; set; }
        public Site? Site { get; set; } = new();
        public Service? Service { get; set; } = new();
    }
}
