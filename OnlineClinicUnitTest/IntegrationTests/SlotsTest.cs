using System;
using System.Linq;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class SlotsTest
	{
		private static readonly DateTime NOW = DateTime.UtcNow;

		[Fact]
		public void ResetDate_NoError()
		{
			var slots = new Slots();
			var today = NOW;

			slots.ResetAllSlots(today);

			Assert.True(true);
		}

		[Fact]
		public void Load_NoError()
		{
			var slots = new Slots();

			slots.Load();
			var allSlots = slots.GetAllLoaded();

			Assert.NotNull(allSlots);
		}

		[Fact]
		public void Update_NoError()
		{
			var slots = new Slots();
			var slot = new Slot()
			{
				PartitionKey = NOW.DayOfWeek.ToString(),
				RowKey = "Slot 1",
				TimeStart = NOW
			};

			slots.Update(slot);
			slots.Load();
			var allSlots = slots.GetAllLoaded();
			var updatedSlot = allSlots.Where(s =>
			{
				return s.PartitionKey == slot.PartitionKey
				&& s.RowKey == slot.RowKey
				&& s.Id == slot.Id
				&& s.TimeStart == slot.TimeStart
				&& s.IsBooked == true;
			});

			Assert.NotNull(updatedSlot);
		}


		public void ff()
		{
			var today = NOW.Date;
			var start = today.AddHours(8);
			var end = today.AddHours(19);

			var week = NOW.DayOfWeek;
			Assert.True(false);
		}
	}
}
