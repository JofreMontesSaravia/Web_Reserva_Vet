namespace Web_Vet_Pet.Models
{
    public class TypePet
    {
        public int Id { get; set; }
        public string animal { get; set; }

        //Propiedad para enlace con mascota
        public List<Pet> Pets { get; set; }
    }
}
