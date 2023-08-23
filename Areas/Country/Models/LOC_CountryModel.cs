using System;
using System.ComponentModel.DataAnnotations;
namespace StudentProject.Areas.Country.Models
{
	public class LOC_CountryModel
	{
		public int? CountryID { get; set; }

		[Required(ErrorMessage = "Country Name is Required")]
		public string CountryName { get; set; }


        [Required(ErrorMessage = "Country Code is Required")]
        public string CountryCode { get; set; }

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Modified { get; set; }
	}
}

