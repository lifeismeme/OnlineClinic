using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
	public class Patient 
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(128)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(128,MinimumLength = 1)]
		public string Name { get; set; }

		[Required]
		[StringLength(128)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[StringLength(16)]
		public string Phone { get; set; }

	}
}
