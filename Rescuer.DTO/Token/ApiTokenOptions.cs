namespace Rescuer.DTO.Token
{
    public class ApiTokenOptions
    {
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
