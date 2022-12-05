using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TravelApp.Common;
using TravelApp.Controllers;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data;
using TravelApp.Data.Entities;
using TravelApp.Data.Repositories;
using static TravelApp.Common.ManageRoles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TravelAppDbContext>
    (options => {
        options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TravelApp.Data"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>
    (
    options => {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<TravelAppDbContext>();

builder.Services.AddControllersWithViews(
    options => {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/ApplicationUsers/Login";
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<ITownService, TownService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IRequestService, RequestService>();


var app = builder.Build();

app.SeedUsersRoles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Home/Error");

}
else
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


app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});

app.MapRazorPages();

app.Run();
