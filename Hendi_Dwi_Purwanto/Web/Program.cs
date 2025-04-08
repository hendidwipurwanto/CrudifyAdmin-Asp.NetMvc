using Application.Middlewares;
using Application.Services;
using Domain;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(Infrastructure.Repositories.IGenericService<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(Application.Services.IGenericService<>), typeof(GenericService<>));

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // <-- ini penting buat Identity & Razor Pages


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // remove the ! to see the details of errors
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error/Server");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseMiddleware<CustomErrorHandlingMiddleware>();
app.UseStatusCodePagesWithReExecute("/Error/HttpStatus", "?code={0}");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=register}/{id?}");
app.MapRazorPages();

app.Run();
