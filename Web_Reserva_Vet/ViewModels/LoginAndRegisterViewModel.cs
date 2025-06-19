namespace Web_Vet_Pet.ViewModels
{
    public class LoginAndRegisterViewModel
    {

        // Propiedad para los datos del formulario de Registro
        public RegisterViewModel Register { get; set; } = new RegisterViewModel();

        // Propiedad para los datos del formulario de Login
        public LoginViewModel Login { get; set; } = new LoginViewModel();
    }
}
