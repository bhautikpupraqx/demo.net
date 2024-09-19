namespace NewMexicoAPI.Models
{
    public class AuthTokenData
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public DateTime issuedDate { get; set; }
        public DateTime expiresDate { get; set; }
    }
}
