using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Models;

namespace OnlineClinic.Controllers
{
	public class ViewAppointmentsController : Controller
	{
		private readonly OnlineClinicContext _context;

		public ViewAppointmentsController(OnlineClinicContext context)
		{
			_context = context;
		}

		// GET: ViewAppointment
		public async Task<IActionResult> Index()
		{
			var onlineClinicContext = _context.Appointment.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Slot);

			
			var appointments = await onlineClinicContext.ToListAsync();
			return View(appointments);
		}

		// GET: ViewAppointment/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointment
				.Include(a => a.Doctor)
				.Include(a => a.Patient)
				.Include(a => a.Slot)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		// GET: ViewAppointment/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointment
				.Include(a => a.Doctor)
				.Include(a => a.Patient)
				.Include(a => a.Slot)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		// POST: ViewAppointment/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var appointment = await _context.Appointment.FindAsync(id);
			_context.Appointment.Remove(appointment);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AppointmentExists(int id)
		{
			return _context.Appointment.Any(e => e.Id == id);
		}
	}
}
