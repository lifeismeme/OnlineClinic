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
		public CloudTableStorage CloudeTableStorage { get; private set; }

		public Slots()
		{
			CloudeTableStorage = new CloudTableStorage();
		}

		public void Dispose()
		{
			CloudeTableStorage?.Dispose();
		}

		public List<Slot> GetAllLoaded()
		{
			return list;
		}

		public void Load()
		{
			list = new List<Slot>();

			var rows = CloudeTableStorage.RetrieveAll();

			foreach (var r in rows)
				list.Add(ParseToSlot(r));
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

		public void ResetAllSlots(DateTime today)
		{
			today = today.ToUniversalTime();
			const int initialOpenHour24f = 8;
			const int closingHour24f = 18;

			const int totalWorkHour = closingHour24f - initialOpenHour24f;
			var weekday = today.DayOfWeek.ToString();
			for (int i = 1; i <= totalWorkHour; ++i)
			{
				var slot = new Slot()
				{
					PartitionKey = weekday,
					RowKey = $"Slot {i}",
					//Id = i,
					TimeStart = today.Date.AddHours(initialOpenHour24f + i),
					Duration = TimeSpan.FromMinutes(30),
					IsBooked = false
				};
				CloudeTableStorage.Insert(slot);
			}

		}

		public void Update(Slot slot)
		{
			if (slot.PartitionKey == null) throw new ArgumentException("PartitionKey is null");
			if (slot.RowKey == null) throw new ArgumentException("RowKey is null");
			if (slot.TimeStart == null) throw new ArgumentException("TimeStart is null");

			slot.IsBooked = true;

			CloudeTableStorage.Insert(slot);
		}
	}
}
