using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;
<<<<<<< HEAD
using Web_Vet_Pet.Repositories;
=======
using Web_Vet_Pet.Services;

>>>>>>> 227170cfd5485aaf86cb21392c155ffc01219fd5

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Obtenemos la cadena de conexion desde el appseting y se almacen Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34)))
);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IValidacionUsers, Validacion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
