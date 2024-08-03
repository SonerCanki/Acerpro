using Acerpro.Common.DTOs.Login;
using Acerpro.Common.DTOs.User;
using Acerpro.Common.Extensions;
using Acerpro.UI.APIs;
using Acerpro.UI.Models.AccountViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acerpro.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;
        public AccountController(
            IAccountApi accountApi,
            IMapper mapper,
            INotyfService notyfService)
        {
            _accountApi = accountApi;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var loginRequest = await _accountApi.Login(_mapper.Map<LoginRequest>(request));
                if (loginRequest.IsSuccessStatusCode && loginRequest.Content.IsSuccess)
                {
                    UserResponse user = loginRequest.Content.ResultData;
                    var claims = new List<Claim>()
                    {
                        new Claim("Id",user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.FirstName),
                        new Claim(ClaimTypes.Surname,user.LastName),
                        new Claim(ClaimTypes.Email,user.Email),
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.Response.Cookies.Append("AcerproToken", System.Text.Json.JsonSerializer.Serialize<UserResponse>(user).Encrypt());
                    await HttpContext.SignInAsync(principal);

                    _notyfService.Success("Giriş İşlemi Başarıyla Gerçekleşti.");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                _notyfService.Error("Lütfen Tüm Alanları Kontrol Edip Tekrar Deneyiniz.");
            }
            return View(request);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("AcerproToken");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
