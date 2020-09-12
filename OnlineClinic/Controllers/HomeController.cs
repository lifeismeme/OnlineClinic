using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Models;

namespace OnlineClinic.Controllers
{
	public class HomeController : Controller
	{
		private OnlineClinicContext _context;

		public HomeController(OnlineClinicContext context)
		{
			_context = context;
		}


		public IActionResult Index()
		{
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
				Debug.WriteLine(ex.Message);
				return NotFound();
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
	}
}
