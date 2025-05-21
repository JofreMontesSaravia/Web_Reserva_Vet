namespace Web_Vet_Pet.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }

        //clave foránea a cliente
        public int ClientId { get; set; }
        //propiedad de navegación (muchas mascotas -> un cliente)
        public Client Client { get; set; }
        //clave foránea a tipo de macota
        public int TypePetId { get; set; }
        //propiedad de navegación (muchas mascotas -> un tipo mascota )
        public TypePet TypePet { get; set; }
        //propiedad de enlace de tablas - cita
        public List<Appointment> Appointments { get; set; }
    }
}
