using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NapocaBike.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;



var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions => { 
    googleOptions.ClientId = configuration["Google:ClientId"];
    googleOptions.ClientSecret = configuration["Google:ClientSecret"];

} );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    policy.RequireRole("Admin"));
    
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/BikeRentalLocations/Index", "AdminPolicy");
    options.Conventions.AuthorizePage("/BikeParkings/Index", "AdminPolicy");
    options.Conventions.AuthorizePage("/Locations/Index", "AdminPolicy");
    options.Conventions.AuthorizePage("/Categories/Index", "AdminPolicy");
    options.Conventions.AuthorizePage("/Members/Index", "AdminPolicy");
    options.Conventions.AuthorizePage("/ReviewProposals", "AdminPolicy");
});




builder.Services.AddDbContext<NapocaBikeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NapocaBikeContext") ?? throw new InvalidOperationException("Connection string 'NapocaBikeContext' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NapocaBikeContext") ?? throw new InvalidOperationException("Connectionstring 'NapocaBikeContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
