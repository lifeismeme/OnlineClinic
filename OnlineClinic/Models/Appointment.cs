using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Appointment
	{
		[Key]
		public int Id { get; set; }

		public int SlotId { get; set; }
		public Slot Slot { get; set; }

		public int DoctorId { get; set; }
		public virtual Doctor Doctor { get; set; }

		public int PatientId { get; set; }
		public virtual Patient Patient { get; set; }

		[Required]
		public bool IsCancelled { get; set; } = false;
	}
}
