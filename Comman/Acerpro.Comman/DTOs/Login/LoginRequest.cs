using Acerpro.Common.Attributes;

namespace Acerpro.Common.DTOs.Login
{
    public class LoginRequest
    {
        [CustomRequired, CustomEmail]
        public string Email { get; set; }
        [CustomRequired]
        public string Password { get; set; }
    }
}
