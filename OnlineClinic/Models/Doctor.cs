using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Doctor : Staff
	{
		public static readonly string DoctorId = "8245fdac-ae34-49bc-afa1-b7a9917d55af";
		public static readonly string DoctorName = "MrDoctor";
		public static Doctor CreateDefaultDoctor()
		{
			return new Doctor()
			{
				AspNetUsersId = DoctorId,
				Name = DoctorName,
				Title = JobTitle.Doctor
			};
		}
	}
}
