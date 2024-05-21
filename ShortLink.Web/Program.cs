using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using ShortLink.Infra.Data.Context;
using ShortLink.Infra.IoC;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ShortLink.Web.Middelware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region IoC
    DependencyContainer.RegisterService(builder.Services);
#endregion

#region DbContext
builder.Services.AddDbContext<ShortLinkDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShortLinkSqlConnection"));
});
#endregion

#region UTF-8
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { 
    UnicodeRanges.BasicLatin,
    UnicodeRanges.Arabic
}));
#endregion

#region authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/log-Out";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});
#endregion

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

#region Call Special MiddleWare, must be after routing
app.UseShortLinkUrl();
#endregion

app.UseRouting();

#region Authentication
app.UseAuthentication();
#endregion
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// routing for middleware
app.MapFallbackToController("Index", "Home");


app.Run();
