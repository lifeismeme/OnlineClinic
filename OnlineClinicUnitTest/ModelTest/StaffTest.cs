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
				AspNetUsersId = "1",
				Name = "DoctorGuy",
				Title = Staff.JobTitle.Doctor
			};

			Assert.Equal("1", s.AspNetUsersId);
			Assert.Equal("DoctorGuy", s.Name);
			Assert.Equal(Staff.JobTitle.Doctor, s.Title);
		}


	}
}
