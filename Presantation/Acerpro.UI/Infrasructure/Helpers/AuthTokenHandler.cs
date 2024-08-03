using Acerpro.Common.DTOs.User;
using Acerpro.Common.Extensions;
using Acerpro.UI.APIs;

namespace Acerpro.UI.Infrasructure.Helpers
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountApi _accountApi;

        public AuthTokenHandler(
            IHttpContextAccessor httpContextAccessor,
            IAccountApi accountApi)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountApi = accountApi;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("AcerproToken"))
            {
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies["AcerproToken"].Decrypt();
                var user = System.Text.Json.JsonSerializer.Deserialize<UserResponse>(cookieData);
                if (user.AccessToken.Expires <= DateTime.Now.ToUnixTime())
                {
                    var loginUrl = "/Account/Login"; 
                    _httpContextAccessor.HttpContext.Response.Redirect(loginUrl);
                }
                else
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.AccessToken.AccessToken);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
