namespace Web_Vet_Pet.Models
{
    public class Client : Person
    {
        public string Status { get; set; }

        // Navegación uno a muchos (un cliente tiene muchas mascotas)
        public ICollection<Pet> Pets { get; set; }
    }
}
