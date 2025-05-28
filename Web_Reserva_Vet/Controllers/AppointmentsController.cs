using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPetRepository _petRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IVeterinarianRepository _veterinarianRepository;

        public AppointmentsController(
            IAppointmentRepository appointmentRepository,
            IPetRepository petRepository,
            IServiceRepository serviceRepository,
            IVeterinarianRepository veterinarianRepository)
        {
            _appointmentRepository = appointmentRepository;
            _petRepository = petRepository;
            _serviceRepository = serviceRepository;
            _veterinarianRepository = veterinarianRepository;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllWithRelationsAsync();
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _appointmentRepository.GetByIdWithRelationsAsync(id.Value);
            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetId,ServiceId,VeterinarianId,DateBooking,StatusBooking")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepository.AddAsync(appointment);
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectLists(appointment.PetId, appointment.ServiceId, appointment.VeterinarianId, appointment.StatusBooking);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _appointmentRepository.GetByIdWithRelationsAsync(id.Value);
            if (appointment == null) return NotFound();

            await LoadSelectLists(appointment.PetId, appointment.ServiceId, appointment.VeterinarianId, appointment.StatusBooking);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetId,ServiceId,VeterinarianId,DateBooking,StatusBooking")] Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAppointment = await _appointmentRepository.GetByIdWithRelationsAsync(id);
                    if (existingAppointment == null) return NotFound();

                    existingAppointment.PetId = appointment.PetId;
                    existingAppointment.ServiceId = appointment.ServiceId;
                    existingAppointment.VeterinarianId = appointment.VeterinarianId;
                    existingAppointment.DateBooking = appointment.DateBooking;
                    existingAppointment.StatusBooking = appointment.StatusBooking;

                    await _appointmentRepository.UpdateAsync(existingAppointment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AppointmentExistsAsync(id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectLists(appointment.PetId, appointment.ServiceId, appointment.VeterinarianId, appointment.StatusBooking);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _appointmentRepository.GetByIdWithRelationsAsync(id.Value);
            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepository.GetByIdWithRelationsAsync(id);
            if (appointment != null)
            {
                await _appointmentRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AppointmentExistsAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            return appointment != null;
        }

        private async Task LoadSelectLists(int? selectedPetId = null, int? selectedServiceId = null, int? selectedVeterinarianId = null, string? selectedStatus = null)
        {
            var pets = await _petRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var veterinarians = await _veterinarianRepository.GetAllAsync();

            var petList = pets
                .Where(p => !string.IsNullOrEmpty(p.Name))
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                .ToList();

            var serviceList = services
                .Where(s => !string.IsNullOrEmpty(s.Name))
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            var veterinarianList = veterinarians
                .Where(v => !string.IsNullOrEmpty(v.Name))
                .Select(v => new SelectListItem { Value = v.Id.ToString(), Text = $"{v.Name} - {v.Specialty}" })
                .ToList();

            var statusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                new SelectListItem { Value = "Confirmada", Text = "Confirmada" },
                new SelectListItem { Value = "Cancelada", Text = "Cancelada" }
            };

            ViewData["PetId"] = new SelectList(petList.Any() ? petList : new List<SelectListItem>(), "Value", "Text", selectedPetId);
            ViewData["ServiceId"] = new SelectList(serviceList.Any() ? serviceList : new List<SelectListItem>(), "Value", "Text", selectedServiceId);
            ViewData["VeterinarianId"] = new SelectList(veterinarianList.Any() ? veterinarianList : new List<SelectListItem>(), "Value", "Text", selectedVeterinarianId);
            ViewData["StatusBooking"] = new SelectList(statusList, "Value", "Text", selectedStatus);
        }
    }
}
