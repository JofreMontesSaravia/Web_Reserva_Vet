using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web_Vet_Pet.Models
{
    public class TypePet
    {
        public int Id { get; set; } //Id Tipo de Mascota

        [Required]
        [StringLength(20)]
        public string Species {  get; set; }
        [Required]
        [StringLength(45)]
        public string Description { get; set; }

        //Propiedad de navegación
        [ValidateNever]
        public ICollection<Pet>? Pets { get; set; }
    }
}
