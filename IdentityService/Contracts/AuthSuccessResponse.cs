namespace IdentityService.Contracts.v1
{
    internal class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        
    }
}