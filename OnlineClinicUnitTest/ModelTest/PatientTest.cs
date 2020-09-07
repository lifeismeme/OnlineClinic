using System;
using OnlineClinic.Models;
using Xunit;

namespace OnlineClinicUnitTest
{
	public class PatientTest
	{
		[Fact]
		public void Init_Values_NotNullAndEqual()
		{
			Patient p = new Patient()
			{
				Id = 1,
				Email = "ggwp@mail.com",
				Name = "MrPotato",
				Password = "etc",
				Phone = "123"
			};

			Assert.Equal(1, p.Id);
			Assert.Equal("ggwp@mail.com", p.Email);
			Assert.Equal("MrPotato", p.Name);
			Assert.Equal("etc", p.Password);
			Assert.Equal("123", p.Phone);
		}

	}
}
