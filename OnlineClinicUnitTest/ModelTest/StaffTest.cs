using System;
using OnlineClinic.Models;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class StaffTest
	{
		[Fact]
		public void InitProperty_Values_NotNullAndCorrect()
		{
			Staff s = new Staff()
			{
				Id = 1,
				Name = "DoctorGuy",
				Title = Staff.JobTitle.Doctor
			};

			Assert.Equal(1, s.Id);
			Assert.Equal("DoctorGuy", s.Name);
			Assert.Equal(Staff.JobTitle.Doctor, s.Title);
		}


	}
}
