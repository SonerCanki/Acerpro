using Acerpro.Common.DTOs.User;
using Acerpro.Common.Model;
using Acerpro.Service.Repository.User;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Acerpro.API.Infrastructor.Models
{
    public class ApiWorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ApiWorkContext(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserResponse CurrentUser
        {
            get
            {
                var authResult = _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme).Result;
                if (!authResult.Succeeded)
                    return null;

                var email = authResult.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                var userId = authResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                var userResult = _userRepository.GetById(Guid.Parse(userId)).Result;
                UserResponse user = _mapper.Map<UserResponse>(userResult);
                return user;
            }
            set
            {
                CurrentUser = value;
            }
        }
    }
}
