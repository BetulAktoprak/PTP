using Microsoft.EntityFrameworkCore;
using PTP.Business.Abstractions;
using PTP.Business.Services;
using PTP.DataAccess;
using PTP.DataAccess.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();


builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<PersonnelRepository>();
builder.Services.AddScoped<ProcessRepository>();
builder.Services.AddScoped<ProcessStageRepository>();
builder.Services.AddScoped<ProjectPersonnelRepository>();
builder.Services.AddScoped<ProcessPersonnelRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<PersonnelService>();
builder.Services.AddScoped<ProcessService>();
builder.Services.AddScoped<ProcessStageService>();
builder.Services.AddScoped<ProjectPersonnelService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProcessPersonnelService>();
builder.Services.AddScoped<IProcessService, ProcessService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
