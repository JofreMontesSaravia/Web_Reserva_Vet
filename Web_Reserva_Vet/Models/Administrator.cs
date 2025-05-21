namespace Web_Vet_Pet.Models
{
    public class Administrator
    {
        // variables que serán componentes de la tabla
        //al heredar valores de user, por defecto su id tambien lo usa
        public int Id { get; set; }

        // instancias para enlaces
        //clave foránea - enlace con user
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
