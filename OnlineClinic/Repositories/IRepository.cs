using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Repositories
{
	public interface IRepository<T> : IDisposable
	{
		void Load();
		void Save();
		List<T> GetAllLoaded();
	}
}
