
namespace identity_client
{
    public static class SampleConfig
    {
        //Server config
        public const string ServerEndpoint = "http://localhost:10051";
        public const string TokenEndpoint = ServerEndpoint + "/connect/token";
        public const string UserInfoEndpoint = ServerEndpoint + "/connect/userinfo";

        //Client(this) Config
        public const string LaunchUrl = "http://localhost:49643"; //this must match launchsettings.json "applicationUrl"
        public const string RedirectEndpoint = LaunchUrl + "/Code";
        public const string ClientId = "client1";
        public const string ClientSecret = "secret";
        public const string Scopes = "openid profile email";
        public const string ResponseType = "code";
    }
}
