using System;
namespace StudentProject.Areas.City.Models
{
	public class LOC_CityModel
	{
		public LOC_CityModel()
		{
		}
		public int? CityID { get; set; }

		public string CityName { get; set; }

		public int StateID { get; set; }

		public int CountyID { get; set; }

		public string CityCode { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Modified { get; set; }
    }
}

