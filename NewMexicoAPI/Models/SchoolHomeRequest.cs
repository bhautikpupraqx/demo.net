namespace NewMexicoAPI.Models
{
    public class SchoolHomeRequest
    {
        public string ReportingPeriodName { get; set; }
        public DateTime CloseDate { get; set; }
        public string DistrictRefId { get; set; }
        public string SchoolList { get; set; }
    }
}
