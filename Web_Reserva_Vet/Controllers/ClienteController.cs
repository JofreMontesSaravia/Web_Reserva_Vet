using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;

namespace Web_Vet_Pet.Controllers
{
    //controller para la vista, aqui van agregando las vistas nuevas respecto a cliente
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
