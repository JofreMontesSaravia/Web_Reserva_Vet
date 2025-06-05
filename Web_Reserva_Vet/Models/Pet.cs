using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web_Vet_Pet.Models
{
    public class Pet
    {
        public int Id { get; set; } //PK Mascota
        public int ClientId { get; set; } //Fk de cliente
        public int TypePetId { get; set; } //Fk de tipo de mascota
        [Required] //requerido si o si
        [StringLength(20)]
        public string Name { get; set; } //nombre
        [Required]
        [StringLength(20)]
        public string Breed { get; set; } //raza
        [Required]
        [Range(1, 30)]
        public int Age { get; set; } //años

        //Propiedad de navegacion
        //según los fk que se maneje
        public TypePet? TypePet { get; set; } 
        public Client? Client { get; set; }

        //Propiedad de navegacion
        [ValidateNever] //No valida su relación con citas para crear una mascota
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
