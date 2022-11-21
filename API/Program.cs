using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Cookies para autenticação

builder.Services.AddAuthentication(opc =>
{
    opc.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opc.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opc.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(opc =>
{
    opc.Cookie.Name = "CookieAutenticacao";
    opc.ExpireTimeSpan = TimeSpan.FromDays(360);
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("CookieAutenticacao", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());

});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
