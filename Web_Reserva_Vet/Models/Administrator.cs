namespace Web_Vet_Pet.Models
{
    public class Administrator
    {
        public int Id { get; set; } //Id Administrador
        public int UserId { get; set; } //Fk

        //propiedad de navegación
        public User Users { get; set; }
    }
}
