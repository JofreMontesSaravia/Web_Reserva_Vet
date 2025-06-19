using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web_Vet_Pet.Models
{
    public class TypePet
    {
        public int Id { get; set; } //PK Tipo de Mascota

        [Required] //requerido si o si
        [StringLength(20)]
        public string Species {  get; set; } //especies
        [Required]
        [StringLength(45)]
        public string Description { get; set; } //descripcion

        //Propiedad de navegación
        [ValidateNever] //No valida su relacíón con mascota al ser creado
        public ICollection<Pet>? Pets { get; set; }
    }
}
