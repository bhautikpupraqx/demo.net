namespace NewMexicoAPI.Models
{
    public class SmallDistrictResponse
    {
        public string DistrictCountErrors { get; set; }
        public string DistrictCountWarnings { get; set; }
        public string DistrictSchoolsCountWithErrors { get; set; }
        public string DistrictSchoolsCountWithWarnings { get; set; }
        public string DistrictSchoolsCountWithZeroErrors { get; set; }
        public string DistrictSchoolsCountWithZeroWarnings { get; set; }
    }
}
