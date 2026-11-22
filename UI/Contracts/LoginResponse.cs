namespace UI.Contracts
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public DateTime ExpiresAtUtc { get; set; }
    
    }
}
