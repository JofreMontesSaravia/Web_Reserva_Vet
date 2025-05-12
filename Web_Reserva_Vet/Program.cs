using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar el servicio de DbContext y la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql("server=localhost;database=VeterinariaDB;user=root;password=12345678;",
    new MySqlServerVersion(new Version(8, 0, 34)))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//con agrega datos y prueban si se registra en el wordbench
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 1? - Insertando un Administrador
    var admin = new Administrator
    {
        Email = "admin@domain.com",
        Password = "admin123",
        Name = "John",
        Last_Name = "Doe",
        Phone = "555-1234",
        Date_Birth = new DateOnly(1985, 1, 1),
        Department = "IT"
    };
    context.Persons.Add(admin);

    // 2? - Insertando un Cliente
    var client = new Client
    {
        Email = "client@domain.com",
        Password = "client123",
        Name = "Jane",
        Last_Name = "Smith",
        Phone = "555-5678",
        Date_Birth = new DateOnly(1990, 5, 20),
        Status = "Activo"
    };
    context.Persons.Add(client);

    // 3? - Insertando un Veterinario
    var vet = new Veterinarian
    {
        Email = "vet@domain.com",
        Password = "vet123",
        Name = "Sam",
        Last_Name = "White",
        Phone = "555-9999",
        Date_Birth = new DateOnly(1988, 10, 10),
        Specialty = "Cirugía"
    };
    context.Persons.Add(vet);

    // Guardamos los cambios
    context.SaveChanges();

    Console.WriteLine("Datos insertados correctamente.");

    // 4? - Consultas
    Console.WriteLine("\n-- Administradores --");
    var admins = context.Persons.OfType<Administrator>().ToList();
    admins.ForEach(a => Console.WriteLine($"{a.Name} {a.Last_Name} - Departamento: {a.Department}"));

    Console.WriteLine("\n-- Clientes --");
    var clients = context.Persons.OfType<Client>().ToList();
    clients.ForEach(c => Console.WriteLine($"{c.Name} {c.Last_Name} - Estado: {c.Status}"));

    Console.WriteLine("\n-- Veterinarios --");
    var vets = context.Persons.OfType<Veterinarian>().ToList();
    vets.ForEach(v => Console.WriteLine($"{v.Name} {v.Last_Name} - Especialidad: {v.Specialty}"));
}

app.Run();
