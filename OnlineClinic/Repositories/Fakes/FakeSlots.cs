using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineClinic.Models;

namespace OnlineClinic.Repositories.Mocks
{
	public class FakeSlots : IRepository<Slot>
	{
		private List<Slot> List { get; set; }

		public void Load()
		{ 
			const int minutes = 30;
			var Today = DateTime.Today;
			List = new List<Slot>()
			{
				new Slot(){Id = 1, TimeStart = Today.AddHours(9), Duration = TimeSpan.FromMinutes(minutes)},
				new Slot(){Id = 2, TimeStart = Today.AddHours(9.5), Duration = TimeSpan.FromMinutes(minutes)},
				new Slot(){Id = 3, TimeStart = Today.AddHours(10), Duration = TimeSpan.FromMinutes(minutes)},
				new Slot(){Id = 4, TimeStart = Today.AddHours(10.5), Duration = TimeSpan.FromMinutes(minutes)},
				new Slot(){Id = 5, TimeStart = Today.AddHours(11), Duration = TimeSpan.FromMinutes(minutes)},
			};
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public List<Slot> GetAllLoaded()
		{
			return List;
		}

		public void Dispose()
		{
			List?.Clear();
		}

		
	}
}
