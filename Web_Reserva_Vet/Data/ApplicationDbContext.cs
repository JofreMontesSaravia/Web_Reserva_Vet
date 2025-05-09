namespace Web_Vet_Pet.Data
{
    // Data/ApplicationDbContext.cs
    using Microsoft.EntityFrameworkCore;
    using Web_Vet_Pet.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }

}
