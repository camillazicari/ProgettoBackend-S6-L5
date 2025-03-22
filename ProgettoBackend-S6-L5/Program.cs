using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.Models;
using ProgettoBackend_S6_L5.Services;
using EcommerceLiveEfCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProgettoBackendDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequiredLength = 8;

    options.Password.RequireDigit = false;

    options.Password.RequireLowercase = false;

    options.Password.RequireUppercase = false;

    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ProgettoBackendDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "Gestionale";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<PrenotazioniService>();
builder.Services.AddScoped<ClientiService>();
builder.Services.AddScoped<PrenotazioniService>();
builder.Services.AddScoped<CamereService>();
builder.Services.AddScoped<AmministrazioneService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
