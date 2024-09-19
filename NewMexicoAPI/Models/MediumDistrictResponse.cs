namespace NewMexicoAPI.Models
{
    public class MediumDistrictResponse
    {
        public IEnumerable<SchoolErrors> SchoolErrorList { get; set; }
        public IEnumerable<SchoolErrorGroups> SchoolErrorGroupsList { get; set; }
        public IEnumerable<SchoolsZeroErrors> SchoolsZeroErrorsList { get; set; }
    }
    public class SchoolErrors
    {
        public string SchoolName { get; set; }
        public int ErrorCount { get; set; }
        public string SchoolRefId { get; set; }
    }

    public class SchoolErrorGroups
    {
        public string ErrorGroup { get; set; }
        public int ErrorCount { get; set; }
    }

    public class SchoolsZeroErrors
    {
        public string SchoolName { get; set; }
    }
}
