namespace Web_Vet_Pet.Models
{
    public class Administrator
    {
        public int Id { get; set; } //PK Administrador
        public int UserId { get; set; } //Fk

        //Propiedad de navegación
        public User? Users { get; set; }
    }
}
