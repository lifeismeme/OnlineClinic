using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Slot
	{
		public int Id { get; set; }
		public DateTime TimeStart { get; set; }
		public TimeSpan Duration { get; set; }
		public DateTime TimeEnd { get { return TimeStart + Duration; } }
	}
}
