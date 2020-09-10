using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Slot
	{
		public static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(30);
		public int Id { get; set; }
		
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime TimeStart { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public TimeSpan Duration { get; set; } = DefaultDuration;
		
		[Required] 
		public bool IsBooked { get; set; } = false;

		public DateTime TimeEnd { get { return TimeStart + Duration; } }
	}
}
