using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Staff
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(128, MinimumLength = 1)]
		public string Name { get; set; }

		[Required]
		[StringLength(128)]
		public JobTitle Title { get; set; }

		public enum JobTitle
		{
			GeneralStaff,
			Doctor
		}
	}
}
