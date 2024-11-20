namespace NewMexicoAPI.Models
{
    public class EmailTemplateForUsersModel
    {
        public int TemplateKey { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public string recurrence_type { get; set; }
        public string recurrence_type_daysOfWeek { get; set; }
        public bool Flag { get; set; }
    }

    public class EmailtemplateforuserList
    {
        public List<EmailTemplateForUsersModel> Usetlistforemailtemplate { get; set; }
    }
}
