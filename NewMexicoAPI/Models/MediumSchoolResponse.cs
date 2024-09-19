namespace NewMexicoAPI.Models
{
    public class MediumSchoolResponse
    {
        public IEnumerable<SchoolErrors> SchoolErrorList { get; set; }
        public IEnumerable<SchoolErrorGroups> SchoolErrorGroupsList { get; set; }
        public IEnumerable<SchoolsZeroErrors> SchoolsZeroErrorsList { get; set; }
    }
}
