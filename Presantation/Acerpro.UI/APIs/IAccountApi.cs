using Acerpro.Common.DTOs.Login;
using Acerpro.Common.DTOs.User;
using Acerpro.Common.Model;
using Refit;

namespace Acerpro.UI.APIs
{
    [Headers("Content-Type: application/json")]
    public interface IAccountApi
    {
        [Post("/login/signIn")]
        Task<ApiResponse<WebApiResponse<UserResponse>>> Login(LoginRequest request);
    }
}
