using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web_Vet_Pet.Models
{
    public class Pet
    {
        public int Id { get; set; } //Id Mascota
        public int ClientId { get; set; } //Fk de cliente
        public int TypePetId { get; set; } //Fk de tipo de mascota
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Breed { get; set; }
        [Required]
        [Range(1, 30)]
        public int Age { get; set; }

        //Propiedad de navegacion
        public TypePet? TypePet { get; set; }
        public Client? Client { get; set; }

        //Propiedad de navegacion
        [ValidateNever]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
