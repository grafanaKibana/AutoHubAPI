namespace AutoHub.BLL.Configuration
{
    public class JwtConfiguration
    {
        public string Key { get; set; }
        public int ExpirationDate { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}