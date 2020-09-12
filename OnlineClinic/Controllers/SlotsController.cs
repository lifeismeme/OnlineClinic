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

		public SlotsController(OnlineClinicContext context)
		{
			_context = context;
		}

		// GET: Slots
		public async Task<IActionResult> Index()
		{
			var list = new List<Slot>();
			using (var slots = new Slots())
			{
				slots.Load();
				list.AddRange(slots.GetAllLoaded());
			}

			return View(list);
		}



		public async Task<IActionResult> Book(string partitionKey, string rowKey)
		{
			try
			{
				var appointment = new Appointment();

				Patient patient = _context.Patient.First(p => p.AspNetUsersId == Patient.CreatePatient(User).AspNetUsersId);
				appointment.Doctor = (Doctor)_context.Staff.FirstOrDefault(s => s.AspNetUsersId == Doctor.DoctorAspNetUsersId);
				appointment.Patient = _context.Patient.First(p => p.Id == patient.Id);
				appointment.IsCancelled = false;

				using (var tbl = new CloudTableStorage())
					appointment.Slot = tbl.Retrieve<Slot>(partitionKey, rowKey);

				_context.Add(appointment);
				_context.SaveChanges();
		

				//return to index
				var list = new List<Slot>();
				using (var slots = new Slots())
				{
					slots.Load();
					list.AddRange(slots.GetAllLoaded());
				}
				return View("Index",list);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return NotFound();
			}
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
			return View();
		}

		// POST: Slots/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,TimeStart,Duration,IsBooked")] Slot slot)
		{
			if (ModelState.IsValid)
			{
				_context.Add(slot);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(slot);
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
