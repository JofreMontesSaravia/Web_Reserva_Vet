namespace Web_Vet_Pet.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Type_Pet { get; set; }

        // Clave foránea para el cliente
        public int ClientId { get; set; }

        // Navegación a la entidad Client
        public Client Client { get; set; }

        // Navegación uno a muchos (una mascota tiene muchas citas)
        public ICollection<Appointment> Appointments { get; set; }
    }
}
