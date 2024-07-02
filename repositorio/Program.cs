using Microsoft.EntityFrameworkCore;
using MVCEF.Repositorio;
using repositorio.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("CadenaSQL");
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString));

// Register repositories and unit of work
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
