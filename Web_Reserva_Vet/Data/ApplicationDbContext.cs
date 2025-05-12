namespace Web_Vet_Pet.Data
{
    // Data/ApplicationDbContext.cs
    using Microsoft.EntityFrameworkCore;
    using Web_Vet_Pet.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // Configuración de relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a muchos entre Client y Pet
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade); //si un cliente es eliminado, todas las pets se van

            // Relación uno a muchos entre Pet y Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación uno a muchos entre Service y Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Si eliminas un servicio, no se eliminan las citas

            // Relación uno a muchos entre Veterinarian y Appointment (opcional)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Veterinarian)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VeterinarianId)
                .OnDelete(DeleteBehavior.SetNull); // Si el veterinario se elimina, la relación queda en null
        }
    }

}
