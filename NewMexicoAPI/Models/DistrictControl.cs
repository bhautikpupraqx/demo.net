namespace NewMexicoAPI.Models
{
    public class DistrictControl
    {
        public string DistrictName { get; set; }
        public long DistrictID { get; set; }
        public Guid DistrictRefId { get; set; }
    }

    public class SSORoles
    {
        public string SSORole { get; set; }
        public string SSORoleRefID { get; set; }
    }
}
