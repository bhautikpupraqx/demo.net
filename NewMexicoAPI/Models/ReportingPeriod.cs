namespace NewMexicoAPI.Models
{
    public class ReportingPeriod
    {
        public int uid { get; set; }
        public string ReportingPeriodName { get; set; }
        public string description { get; set; }
        public string reportID { get; set; }
        public int bAlways { get; set; }
        public DateTime sDate { get; set; }
        public DateTime eDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int isVisible_UI { get; set; }
        public int SortOrder_UI { get; set; }
    }
}
