using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Appointment
	{
		public int Id { get; set; }

		[Required]
		public Slot Slot { get; set; }

		[Required]
		public Staff Doctor { get; set; }

		[Required]
		public Patient Patient { get; set; }

		[Required]
		public bool IsCancelled { get; set; } = false;
	}
}
