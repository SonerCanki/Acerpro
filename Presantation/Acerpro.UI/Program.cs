using Acerpro.UI.APIs;
using Acerpro.UI.Infrasructure.Helpers;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Polly;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
    });

RegisterClients(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void RegisterClients(IServiceCollection services)
{
    //RegisterHttpHandler
    services.AddScoped<AuthTokenHandler>();

    var baseUrl = configuration.GetSection("Settings").GetSection("Host").GetSection("CoreAPIServer").Value;
    var baseUri = new Uri(baseUrl);

    services.AddRefitClient<IAccountApi>()
        .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
        .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

    services.AddRefitClient<IEmployeeApi>()
        .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
        .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
        .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());
    
    services.AddRefitClient<IDepartmentApi>()
        .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
        .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
        .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

}