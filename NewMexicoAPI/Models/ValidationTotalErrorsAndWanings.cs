namespace NewMexicoAPI.Models
{
    public class ValidationTotalErrorsAndWanings
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public string SchoolInfoRefId { get; set; }
    }
}
