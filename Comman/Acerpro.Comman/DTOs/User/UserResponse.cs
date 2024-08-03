using Acerpro.Common.Model;

namespace Acerpro.Common.DTOs.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public GetAccessToken AccessToken { get; set; }
    }
}
