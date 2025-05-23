namespace Web_Vet_Pet.Data
{
    using System.Reflection.Emit;
    // Data/ApplicationDbContext.cs
    using Microsoft.EntityFrameworkCore;
    using Web_Vet_Pet.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TypePet> TypePets { get; set; }

        // Configuración de relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos: User → Clients
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Users)
                .WithMany(u => u.Clients)
                .HasForeignKey(c => c.UserId);

            // Relación uno a muchos: User → Administrators
            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.Users)
                .WithMany(u => u.Administrators)
                .HasForeignKey(a => a.UserId);

            // Relación uno a muchos: Client → Pets
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.ClientId);

            // Relación uno a muchos: TypePet → Pets
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.TypePet)
                .WithMany(t => t.Pets)
                .HasForeignKey(p => p.TypePetId);

            // Relación uno a muchos: Pet → Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId);

            // Relación uno a muchos: Service → Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);

            // Relación uno a muchos: Veterinarian → Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Veterinarian)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VeterinarianId);
        }
    }

}
