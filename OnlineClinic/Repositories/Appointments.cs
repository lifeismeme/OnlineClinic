using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineClinic.Models;

namespace OnlineClinic.Repositories
{
	public class Appointments : IRepository<Appointment>
	{
		private OnlineClinicContext _context;
		private List<Appointment> LoadedList { get; set; }

		public Appointments(OnlineClinicContext context)
		{
			_context = context;
		}


		public void Dispose()
		{
			LoadedList?.Clear();
		}

		public List<Appointment> GetAllLoaded()
		{
			return LoadedList;
		}

		public void Load()
		{
			IQueryable<Appointment> TypeQuery = from a in _context.Appointment
												select a;
			LoadedList = TypeQuery.ToList<Appointment>();
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
