using System;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Areas.State.Models
{
	public class LOC_StateModel
	{
		public LOC_StateModel()
		{
		}
		public int? StateId { get; set; }

        [Required(ErrorMessage = "State Name is Required")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "State Code is Required")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "Country Id is Required")]
        public int CountryId { get; set; }

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Modified { get; set; }
	}
}

