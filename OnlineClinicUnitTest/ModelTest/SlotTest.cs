using System;
using OnlineClinic.Models;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class SlotTest
	{
		[Fact]
		public void InitProperty_Values_NotNullAndCorrect()
		{
			DateTime Now = DateTime.Now;
			Slot slot = new Slot()
			{
				Id = 1,
				TimeStart = Now,
				Duration = TimeSpan.FromHours(1)
			};

			Assert.Equal(Now, slot.TimeStart);
			Assert.Equal(TimeSpan.FromHours(1), slot.Duration);
			Assert.Equal(slot.TimeStart + slot.Duration, slot.TimeEnd);
		}
				
	}
}
