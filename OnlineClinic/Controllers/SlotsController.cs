using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Models;
using OnlineClinic.Repositories;

namespace OnlineClinic.Controllers
{
	public class SlotsController : Controller
	{
		private readonly OnlineClinicContext _context;
		private readonly Slots slots;

		private readonly ServiceBus sb;

		public SlotsController(OnlineClinicContext context)
		{
			_context = context;

			slots = new Slots();

			sb = new ServiceBus();
			sb.SetHandler(
				ServiceBus.DefaultMessageHandler,
				ServiceBus.DefaultErrorHandler
				);
		}

		// GET: Slots
		public async Task<IActionResult> Index()
		{
			if (!User.Identity.IsAuthenticated)
				return Redirect("Identity/Account/Login");

			var list = new List<Slot>();
			slots.Load();

			DateTime now = DateTime.Now;
			list.AddRange(slots.GetAllLoaded().Where(s => s.TimeStart >= now));

			return View(list.OrderBy(s => s.TimeStart));
		}

		public async Task<IActionResult> Book(string partitionKey, string rowKey)
		{
			if (!User.Identity.IsAuthenticated)
				return Redirect("Identity/Account/Login");
			try
			{
				var tbl = slots.CloudeTableStorage;

				var slot = tbl.Retrieve<Slot>(partitionKey, rowKey);
				slot.Id = 0;
				if (slot.IsBooked)
					return RedirectToAction("Index");
				slot.IsBooked = true;

				_context.Add(GetAppointment(slot));
				Task<int> saving = _context.SaveChangesAsync();
				slots.Update(slot);

				await saving;
				sb.Send($"User: {User.Identity.Name} booked the slot: p[{slot.PartitionKey}] r[{slot.RowKey}] v[{slot.TimeStart}]");
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				return RedirectToAction("Error", "Home");
			}
		}

		private Appointment GetAppointment(Slot slot)
		{
			var appointment = new Appointment();

			appointment.Slot = slot;

			Patient patient = _context.Patient.First(p => p.AspNetUsersId == Patient.CreatePatient(User).AspNetUsersId);
			appointment.Doctor = (Doctor)_context.Staff.FirstOrDefault(s => s.AspNetUsersId == Doctor.DoctorAspNetUsersId);
			appointment.Patient = _context.Patient.First(p => p.Id == patient.Id);
			appointment.IsCancelled = false;

			return appointment;
		}

		// GET: Slots/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var slot = await _context.Slot
				.FirstOrDefaultAsync(m => m.Id == id);
			if (slot == null)
			{
				return NotFound();
			}

			return View(slot);
		}

		// GET: Slots/Create
		public IActionResult Create()
		{
			if (!Doctor.IsDoctor(User)) return NotFound();

			return View();
		}

		// POST: Slots/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DateTime date)
		{
			if (!Doctor.IsDoctor(User)) return NotFound();

			date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

			slots.ResetAllSlots(date);

			return View();
		}

		// GET: Slots/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var slot = await _context.Slot.FindAsync(id);
			if (slot == null)
			{
				return NotFound();
			}
			return View(slot);
		}

		// POST: Slots/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStart,Duration,IsBooked")] Slot slot)
		{
			if (id != slot.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(slot);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SlotExists(slot.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(slot);
		}

		// GET: Slots/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var slot = await _context.Slot
				.FirstOrDefaultAsync(m => m.Id == id);
			if (slot == null)
			{
				return NotFound();
			}

			return View(slot);
		}

		// POST: Slots/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var slot = await _context.Slot.FindAsync(id);
			_context.Slot.Remove(slot);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SlotExists(int id)
		{
			return _context.Slot.Any(e => e.Id == id);
		}
	}
}
