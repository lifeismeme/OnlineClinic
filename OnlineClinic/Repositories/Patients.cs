using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineClinic.Models;

namespace OnlineClinic.Repositories
{
	public class Patients : IRepository<Patient>
	{
		private OnlineClinicContext _context;
		private List<Patient> LoadedList { get; set; }

		public Patients(OnlineClinicContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
			LoadedList?.Clear();
		}

		public List<Patient> GetAllLoaded()
		{
			return LoadedList;
		}

		public void Load()
		{
			IQueryable<Patient> TypeQuery = from a in _context.Patient
											select a;
			LoadedList = TypeQuery.ToList<Patient>();
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
