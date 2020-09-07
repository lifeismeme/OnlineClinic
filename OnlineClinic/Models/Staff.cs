using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Staff
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public JobTitle Title { get; set; }

		public enum JobTitle
		{
			GeneralStaff,
			Doctor
		}
	}
}
