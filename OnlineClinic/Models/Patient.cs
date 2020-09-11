using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineClinic.Areas.Identity.Data;

namespace OnlineClinic.Models
{
	public class Patient
	{
		public int Id { get; set; }

		[Required]
		public string UID { get; set; }


		[StringLength(128)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(128, MinimumLength = 1)]
		public string Name { get; set; }


		[StringLength(128)]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[StringLength(16)]
		public string Phone { get; set; }

		public static Patient CreatePatient(ClaimsPrincipal User)
		{
			return new Patient()
			{
				UID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
				Name = User.Identity.Name
			};
		}
	}
}
