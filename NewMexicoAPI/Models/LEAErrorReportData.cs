namespace NewMexicoAPI.Models
{
    public class LEAErrorReportData
    {
        public int uid { get; set; }
        public string RefId { get; set; }
        public string ObjectType { get; set; }
        public string StateProvinceId { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorDesc { get; set; }
        public string ErrorType { get; set; }
        public int ValidationLevel { get; set; }
        public string actualVals { get; set; }
        public string rName { get; set; }
        public string ErrorGroup { get; set; }
        public string DistrictName { get; set; }
        public string SchoolName { get; set; }
        public string pCodeMsg { get; set; }
        public string ZoneTag { get; set; }
        public string LEAInfoRefId { get; set; }
        public string SchoolInfoRefId { get; set; }
        public string StudentPersonalRefId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
