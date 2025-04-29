using Microsoft.EntityFrameworkCore;
using PTP.Business.Services;
using PTP.DataAccess;
using PTP.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<PersonnelRepository>();
builder.Services.AddScoped<ProcessRepository>();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<PersonnelService>();
builder.Services.AddScoped<ProcessService>();
builder.Services.AddScoped<UserService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
