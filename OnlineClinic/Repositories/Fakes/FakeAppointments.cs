using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineClinic.Models;

namespace OnlineClinic.Repositories.Fakes
{
	public class FakeAppointments : IRepository<Appointment>
	{
		private List<Appointment> List { get; set; }
		public void Dispose()
		{
			List?.Clear();
		}

		public List<Appointment> GetAllLoaded()
		{
			return List;
		}

		public void Load()
		{
			List = new List<Appointment>() {
				new Appointment(){
					Id = 1,
					Slot = new Slot(){Id = 44, TimeStart = DateTime.Today, Duration=TimeSpan.FromMinutes(40)},
					IsCancelled = false
				},
				new Appointment(){
					Id = 2,
					Slot = new Slot(){Id = 45, TimeStart = DateTime.Today.AddDays(1), Duration=TimeSpan.FromMinutes(40)},
					IsCancelled = true
				}
			};
		}

		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}
