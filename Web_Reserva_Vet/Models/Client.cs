using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_Vet_Pet.Models
{
    public class Client
    {
        [Key] // Es buena práctica añadir [Key] también
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //PK Cliente
        public int UserId { get; set; }//Fk

        //Propiedad de navegacion
        [ValidateNever] //No valida su relación con pet para ser creado
        public ICollection<Pet> Pets { get; set; }

        //Propiedad de navegación
        public User? Users { get; set; }
    }
}
