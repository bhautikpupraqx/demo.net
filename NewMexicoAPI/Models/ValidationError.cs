namespace NewMexicoAPI.Models
{
    public class ValidationError
    {
        public int uid { get; set; }
        public Guid RefId { get; set; }
        public string ObjectType { get; set; }
        public string StateProvinceId { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorDesc { get; set; }
        public string ErrorType { get; set; }
        public string ValidationLevel { get; set; }
        public string actualVals { get; set; }
        public string rName { get; set; }
        public string ErrorGroup { get; set; }
        public string DistrictName { get; set; }
        public string SchoolName { get; set; }
        public string pCodeMsg { get; set; }
        public Guid LEAInfoRefId { get; set; }
        public Guid SchoolInfoRefId { get; set; }
        public Guid StudentPersonalRefId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
