using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web_Vet_Pet.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(16)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(9)]
        public string Phone { get; set; }
        public DateOnly DateBirthday { get; set; }

        //Propiedad de navegación
        [ValidateNever]
        public ICollection<Client>? Clients { get; set; }
        [ValidateNever]
        public ICollection<Administrator>? Administrators { get; set; }
    }
}
