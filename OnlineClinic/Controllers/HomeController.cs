using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using static OnlineClinic.Models.Staff;

namespace OnlineClinic.Controllers
{
	public class HomeController : Controller
	{
		private OnlineClinicContext _context;

		private readonly ServiceBus sb;
		public HomeController(OnlineClinicContext context)
		{
			_context = context;

			setFirstPatientAsDoctor();
			setDefaultDoctor();

			sb = new ServiceBus();
			sb.SetHandler(
				ServiceBus.DefaultMessageHandler,
				ServiceBus.DefaultErrorHandler
				);
		}

		private void setFirstPatientAsDoctor()
		{
			Patient p = _context.Patient.FirstOrDefault();
			if (p == null)
				return;

			if (_context.Staff.FirstOrDefault() != null)
				return;

			var staff = new Doctor() { 
				AspNetUsersId = p.AspNetUsersId,
				Name = p.Name,
				Title = JobTitle.Doctor,
			};
			_context.Add(staff);
			_context.SaveChanges();
		}

		private void setDefaultDoctor()
		{
			Staff doctor = _context.Staff.FirstOrDefault();
			if (doctor == null)
				return;

			Doctor.DoctorAspNetUsersId = doctor.AspNetUsersId;
			Doctor.DoctorName = doctor.Name;
		}

		public IActionResult Index()
		{
			string aUser;
			if (User.Identity.IsAuthenticated)
				aUser = User.Identity.Name;
			else
				aUser = "a User";

			sb.Send(Encoding.UTF8.GetBytes($"Info: {aUser} visit Home Page! time: " + DateTime.Now));
			return View();
		}

		public IActionResult Register(User user)
		{
			try
			{
				//add to custom table

				_context.Add(new Patient()
				{
					AspNetUsersId = user.Id,
					Password = user.PasswordHash,
					Email = user.Email,
					Name = user.UserName
				});
				_context.SaveChanges();


				return View("Index");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				return RedirectToAction("Error");
			}

		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";
			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Exception()
		{
			throw new Exception("- test sample Exception -");
		}
	}
}
