namespace NewMexicoAPI.Models
{
    public class LoggedInUserDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrgCode { get; set; }
        public string District { get; set; }
        public string DistrictId { get; set; }
        public string? DistrictRefId { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public string Schools { get; set; }
        public string SchoolRefIds { get; set; }
        public string Email { get; set; }
    }
}
