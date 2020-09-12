using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using OnlineClinic.Models;

namespace OnlineClinic.Repositories
{
	public class Slots : IRepository<Slot>
	{
		private List<Slot> list;

		public void Dispose()
		{
			list?.Clear();
		}

		public List<Slot> GetAllLoaded()
		{
			return list;
		}

		public void Load()
		{
			list = new List<Slot>();
			using (var tbl = new CloudTableStorage())
			{
				var rows = tbl.RetrieveAll();

				foreach (var r in rows)
					list.Add(ParseToSlot(r));
			}
		}

		private Slot ParseToSlot(DynamicTableEntity row)
		{
			var p = row.Properties;
			return new Slot()
			{
				PartitionKey = row.PartitionKey,
				RowKey = row.RowKey,
				TimeStart = p["TimeStart"].DateTime ?? new DateTime(),
				IsBooked = p["IsBooked"].BooleanValue ?? false
			};
		}

		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}
