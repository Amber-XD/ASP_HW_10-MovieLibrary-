using System.ComponentModel.DataAnnotations;

namespace UI.Contracts
{
    public class IdentityLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
