using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.DTOs; // Para UsuarioSeleccionDto
using Web_Vet_Pet.Models; // Para Usuario
using System.Linq; // Para LINQ
using System.Collections.Generic; // Para List
using System.Threading.Tasks; // Para Task


namespace Web_Vet_Pet.Services
{
   
    public interface IValidacionUsers
    {
        Task<List<Usuarios>> ObtenerSeleccionUsuarioDisponiblesync();
    }

    public class Validacion: IValidacionUsers
    {

        private readonly ApplicationDbContext _context;

        public Validacion(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuarios>> ObtenerSeleccionUsuarioDisponiblesync()
        {
             //Para obtener los  Usuarios que son clientes
            var idClientes = await _context.Clients.Select(c => c.UserId).Distinct().ToListAsync();

            //Para obtener los Usuarios que son Administradores

            var idAdministradores = await _context.Administrators.Select(a => a.UserId).Distinct().ToListAsync();

            //Todo los id usuarios ocupados
            var idUsuariosOcupados = idClientes.Union(idAdministradores);


            //todos los id Disponibles
            var cmb = await _context.Users.Where(u => !idUsuariosOcupados.Contains(u.Id)).Select(a => new Usuarios {

                Id = a.Id,
                email = a.Email
            })
                .ToListAsync(); 


            return cmb;
        }
    }
}
