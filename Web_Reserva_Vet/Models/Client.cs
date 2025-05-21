namespace Web_Vet_Pet.Models
{
    public class Client
    {
        // variables que serán componentes de la tabla
        //al heredar valores de user, por defecto su id tambien lo usa
        public int Id { get; set; }

        // instancias para enlaces
        //clave foránea - enlace con user
        public int  UserId { get; set; }
        public User User { get; set; }

        //enlace con mascota
        //propiedad de navegación (1 cliente - muchas mascotas)
        public List<Pet> Pets { get; set; }
    }
}
