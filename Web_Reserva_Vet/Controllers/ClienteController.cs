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
        public IActionResult Perfil()
        {
            return View();
        }
        public IActionResult Reserva()
        {
            return View();
        }
        // GET: AjustesController
        public ActionResult Ajustes()
        {
            return View();
        }

        // GET: AjustesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AjustesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AjustesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AjustesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AjustesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AjustesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AjustesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
