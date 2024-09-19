namespace NewMexicoAPI.Models
{
    public class ValidationErrorRequest
    {
        public string DistrictRefId { get; set; }
        public string ReprotingPeriodName { get; set; }
        public DateTime CloseDate { get; set; }
        public string SchoolList { get; set; }
        public string ValidationErrorType { get; set; }
        public string[] SelectedSchoolInfoRefIds { get; set; }
    }
}
