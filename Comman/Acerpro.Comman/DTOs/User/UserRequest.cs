using Acerpro.Common.Attributes;

namespace Acerpro.Common.DTOs.User
{
    public class UserRequest
    {
        [CustomRequired,CustomEmail]
        public string Email { get; set; }
        [CustomRequired]
        public string Password { get; set; }
    }
}
