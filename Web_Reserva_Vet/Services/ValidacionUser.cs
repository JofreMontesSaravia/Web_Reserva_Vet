using Web_Vet_Pet.Interfaces;

namespace Web_Vet_Pet.Services
{
    public class ValidacionUser : IValidacionUsers
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAdministratorRepository _administratorRepository;

        public ValidacionUser(IUserRepository userRepository, IClientRepository clientRepository, IAdministratorRepository administratorRepository)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _administratorRepository = administratorRepository;
        }

        public async Task<List<Usuarios>> ObtenerSeleccionUsuarioDisponiblesync()
        {
            // Obtener IDs de usuarios que son clientes
            var clients = await _clientRepository.GetAllAsync();
            var idClientes = clients.Select(c => c.UserId).Distinct().ToList();

            // Obtener IDs de usuarios que son administradores
            var administrators = await _administratorRepository.GetAllAsync();
            var idAdministradores = administrators.Select(a => a.UserId).Distinct().ToList();

            // Combinar IDs ocupados
            var idUsuariosOcupados = idClientes.Union(idAdministradores);

            // Obtener usuarios no ocupados
            var users = await _userRepository.GetAllAsync();
            var cmb = users
                .Where(u => !idUsuariosOcupados.Contains(u.Id))
                .Select(u => new Usuarios
                {
                    Id = u.Id,
                    email = u.Email
                })
                .ToList();

            return cmb;
        }
    }

    public class Usuarios
    {
        public int Id { get; set; }
        public string email { get; set; }
    }
}
