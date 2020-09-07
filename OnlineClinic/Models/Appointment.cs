using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Appointment
	{
		public int Id { get; set; }
		public Slot Slot { get; set; }
		public Patient Patient { get; set; }
	}
}
