namespace BookRental.Server.Models.UI
{
    public class JWTCredentialsSettings
    {
        public required string ValidAudience { get; set; }
        public required string ValidIssuer { get; set; }
        public required string Secret { get; set; }
    }
}
