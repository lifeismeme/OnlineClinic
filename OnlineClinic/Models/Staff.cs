using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Staff
	{
		public static readonly string DoctorId = "8245fdac-ae34-49bc-afa1-b7a9917d55af";
		public static readonly string DoctorName = "MrDoctor";

		public int Id { get; set; }

		[Required]
		public string UID { get; set; }

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

		public static Staff CreateDefaultDoctor()
		{
			return new Staff()
			{
				UID = DoctorId,
				Name = DoctorName,
				Title = JobTitle.Doctor
			};
		}
	}
}
