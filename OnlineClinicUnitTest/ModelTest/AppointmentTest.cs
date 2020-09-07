using System;
using OnlineClinic.Models;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class AppointmentTest
	{
		[Fact]
		public void InitProperty_Values_NotNullAndCorrect()
		{
			DateTime now = DateTime.Now;
			Appointment ap = new Appointment() {
				Id = 1,
				Slot = new Slot(),
				Patient = new Patient()
			};

			Assert.True(ap.Id == 1);
			Assert.NotNull(ap.Slot);
			Assert.NotNull(ap.Patient);
		}

		
	}
}
