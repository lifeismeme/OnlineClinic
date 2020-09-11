using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using OnlineClinic.Repositories.Fakes;
using OnlineClinic.Repositories.Mocks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace OnlineClinic.Controllers
{
	public class AppointmentsController : Controller
	{
		private readonly OnlineClinicContext _context;

		public AppointmentsController(OnlineClinicContext context)
		{
			_context = context;
		}

		// GET: Appointments
		public async Task<IActionResult> Index()
		{
			IQueryable<Appointment> appointments = from a in _context.Appointment
					 join p in _context.Patient on a.Patient.Id equals p.Id into ap
					 select a;

			return View(appointments);

		}

		// GET: Appointments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();


			var appointment = await _context.Appointment
				.FirstOrDefaultAsync(m => m.Id == id);
			if (appointment == null) return NotFound();

			return View(appointment);
		}

		// GET: Appointments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Appointments/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Appointment appointment)
		{
			if (appointment.Slot == null)
				return NotFound();

			appointment.Doctor = Staff.CreateDefaultDoctor();
			appointment.Patient = Patient.CreatePatient(User);
			appointment.IsCancelled = false;

			_context.Add(appointment);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Cancel(int? id)
		{
			if (id == null)
				return NotFound();

			var appointment = await _context.Appointment
				.FirstOrDefaultAsync(m => m.Id == id);
			if (appointment == null)
				return NotFound();
			return View(appointment);
		}

		// POST: Appointments/Cancel/5
		[HttpPost, ActionName("Cancel")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CancelConfirmed(int id)
		{
			var appointment = await _context.Appointment.FindAsync(id);
			appointment.IsCancelled = true;
			_context.Appointment.Update(appointment);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AppointmentExists(int id)
		{
			return _context.Appointment.Any(e => e.Id == id);
		}
	}
}
