using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Models;
using System.Diagnostics;
using OnlineClinic.Repositories;

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
			//var patientAspNetUsersId = Patient.CreatePatient(User).AspNetUsersId;
			//var patient = (from p in _context.Patient
			//			   where p.AspNetUsersId == patientAspNetUsersId
			//			   select p).First();
			//IQueryable<Appointment> allAppointments =
			//	from a in _context.Appointment
			//	where a.PatientId == patient.Id && a.IsCancelled == false
			//	select a;

			//var appointments = allAppointments.ToList<Appointment>();
			//appointments.ForEach(a => a.Patient = patient);

			//appointments.ForEach(a => a.Slot = (from s in _context.Slot
			//									where s.Id == a.SlotId
			//									select s).First());


			var appointments = _context.Appointment.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Slot).ToList();
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
			try
			{
				Patient patient = _context.Patient.First(p => p.AspNetUsersId == Patient.CreatePatient(User).AspNetUsersId);
				//appointment.Slot = _context.Slot.First(s => s.TimeStart == appointment.Slot.TimeStart);
				appointment.Doctor = (Doctor)_context.Staff.FirstOrDefault(s => s.AspNetUsersId == Doctor.DoctorAspNetUsersId);
				appointment.Patient = _context.Patient.First(p => p.Id == patient.Id);
				appointment.IsCancelled = false;

				_context.Add(appointment);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return NotFound();
			}

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
