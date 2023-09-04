using System;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Areas.City.Models
{
	public class LOC_CityModel
	{
		public int? CityID { get; set; }

        [Required(ErrorMessage = "City Name is Required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "State Name is Required")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "City Code is Required")]
        public string CityCode { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime Modified { get; set; }
    }
}

