using System;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class CloudTableStorageTest
	{
		private static readonly DateTime NOW = DateTime.UtcNow;
		private Slot SampleSlot()
		{
			return new Slot()
			{
				PartitionKey = NOW.DayOfWeek.ToString(),
				RowKey = "Slot 1",
				TimeStart = NOW,
				Duration = TimeSpan.FromMinutes(30)
			};
		}

		[Fact]
		public void Insert_Slot_NoError()
		{
			var tbl = new CloudTableStorage();
			var slot = SampleSlot();

			tbl.Insert(slot);
			Assert.True(true);
		}

		[Fact]
		public void Update_Slot_NoError()
		{
			var tbl = new CloudTableStorage();
			var slot = SampleSlot();
			slot.IsBooked = true;

			tbl.Update(slot);

			Assert.True(true);
		}

		[Fact]
		public void Retrieve_PartitionKeyAndRowKey_Slot()
		{
			var tbl = new CloudTableStorage();
			var sampleSlot = SampleSlot();
			
			var actualSlot = tbl.Retrieve<Slot>(sampleSlot.PartitionKey, sampleSlot.RowKey);

			Assert.NotNull(actualSlot);
			Assert.IsType<Slot>(actualSlot);
		}

		[Fact]
		public void RetrieveAll_NoError()
		{
			var tbl = new CloudTableStorage();
			var rows = tbl.RetrieveAll();

			foreach (var r in rows)
			{
				Assert.NotNull(r);
				var p = r.Properties;
				var slot = new Slot()
				{
					PartitionKey = r.PartitionKey,
					RowKey = r.RowKey,
					TimeStart = p["TimeStart"].DateTime ?? new DateTime(),
					IsBooked = p["IsBooked"].BooleanValue ?? false
				};
				Assert.NotNull(slot);
			}
		}

	}
}
