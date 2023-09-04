using System;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Areas.State.Models
{
	public class LOC_StateModel
	{
		public int? StateID { get; set; }

        [Required(ErrorMessage = "State Name is Required")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "State Code is Required")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        public int CountryID { get; set; }

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Modified { get; set; }

		public class StateDropDownModel
		{
			public int StateID { get; set; }
			public string? StateName { get; set; }
		}
	}
}

