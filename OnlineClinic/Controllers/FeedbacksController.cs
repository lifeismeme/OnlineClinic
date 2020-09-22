using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineClinic.Repositories;

namespace OnlineClinic.Controllers
{
	public class FeedbacksController : Controller
	{
		private ServiceBus sb;

		public FeedbacksController()
		{
			sb = new ServiceBus();
			sb.SetHandler(
				ServiceBus.DefaultMessageHandler,
				ServiceBus.DefaultErrorHandler
				);
		}
		// GET: FeedbackController
		public ActionResult Index()
		{
			return View();
		}

		
		// POST: FeedbackController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(string txtFeedback)
		{
			if(string.IsNullOrWhiteSpace(txtFeedback))
				return  RedirectToAction("Error", "Home");
			if(!User.Identity.IsAuthenticated)
				return  Redirect("Identity/Account/Login");

			sb.Send($"[Feedback] {DateTime.Now} - user: {User.Identity.Name} says: {txtFeedback}");

			return RedirectToAction("Index", "Feedbacks");
		}

	}
}
