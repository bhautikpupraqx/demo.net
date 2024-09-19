namespace NewMexicoAPI.Models
{
    public class ErrorResponse
    {
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorShortDiscription { get; set; }
        public int ErrorCount { get; set; }
    }
}
