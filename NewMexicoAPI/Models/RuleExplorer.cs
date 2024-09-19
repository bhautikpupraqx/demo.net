namespace NewMexicoAPI.Models
{
    public class RuleExplorer
    {
        public string RefId { get; set; }
        public string itemName { get; set; }
        public string actionRefid { get; set; }
        public string connectionRefid { get; set; }
        public string errorType { get; set; }
        public string errorMessage { get; set; }
        public string errorRefid { get; set; }
        public string categoryRefid { get; set; }
        public string objectRefid { get; set; }
        public bool successValue { get; set; }
        public bool bNotifyMessage { get; set; }
        public string notifyRefid { get; set; }
        public string notifyMessage { get; set; }
        public string errorGroup { get; set; }
        public bool exceptionFlag { get; set; }
        public string exceptionType { get; set; }
        public int validationLevel { get; set; }
        public bool pCodeFlag { get; set; }
        public string reportIdList { get; set; }
        public string errorTitle { get; set; }
        public string ErrorShortDescription { get; set; }
        public string pCodeMsg { get; set; }
    }
}
