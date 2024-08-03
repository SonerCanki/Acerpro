using Acerpro.Common.DTOs.Login;
using Acerpro.Common.DTOs.User;
using Acerpro.Common.Extensions;
using Acerpro.Common.Model;
using Acerpro.Service.Repository.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Acerpro.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginController(
            IUserRepository userRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("signIn")]
        public async Task<WebApiResponse<UserResponse>> SignIn([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.GetByDefault(x => x.Email == request.Email && x.Password == request.Password);
                if (result != null)
                {
                    UserResponse rm = _mapper.Map<UserResponse>(result);
                    rm.AccessToken = SetAccessToken(rm, request.Password);
                    return new WebApiResponse<UserResponse>(true, "Success", rm);
                }
            }
            return new WebApiResponse<UserResponse>(false, "");
        }


        private GetAccessToken SetAccessToken(UserResponse rm, string password)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,rm.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,rm.Email)
            };

            var systemSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var signingCredentials = new SigningCredentials(systemSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Tokens:Expires"]));
            var ticks = expires.ToUnixTime();


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return new GetAccessToken
            {
                TokenType = "Acerpro",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expires = ticks,
            };
        }
    }
}
