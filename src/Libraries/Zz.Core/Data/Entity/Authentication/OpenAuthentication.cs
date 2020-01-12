namespace Zz.Core.Data.Entity.Authentication
{
    public class OpenAuthentication : BaseEntity
    {
        public string AppKey { get; set; }
        public string OAuthToken { get; set; }
        public string HttpHost { get; set; }
        public string SsoInUrl { get; set; }
        public string ValidateTokenUrl { get; set; }
    }
}
