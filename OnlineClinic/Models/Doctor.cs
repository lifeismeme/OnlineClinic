using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Doctor : Staff
	{
		public static readonly string DoctorId = "ba222a87-d888-49c2-a648-668a8d8dfb15";
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

		public static bool IsDoctor(ClaimsPrincipal User)
		{
			return User.FindFirst(ClaimTypes.NameIdentifier).Value == DoctorId;
		}
	}
}
