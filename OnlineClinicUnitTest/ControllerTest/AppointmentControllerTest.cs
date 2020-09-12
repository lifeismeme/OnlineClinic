using System;
using Moq;
using OnlineClinic.Controllers;
using OnlineClinic.Models;
using Xunit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;



namespace OnlineClinicUnitTest
{
	public class AppointmentControllerTest 
	{
		
		[Fact]
		public void Create_Appointment_RedirectToActionResult()
		{
			var mockContext = new Mock<OnlineClinicContext>();
			var controller = new AppointmentsController(mockContext.Object);
			var appointment = new Appointment() {
				Id = 1,
				Doctor = new Doctor(),
				Patient = new Patient(),
				Slot = new Slot()
			};

			var task = controller.Create(appointment);

			Assert.IsType<RedirectToActionResult>(task.Result);
		}

		
	}
}
