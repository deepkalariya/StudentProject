using System;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Areas.Student.Models
{
	public class StudentModel
	{
		public int? StudentID { get; set; }

		[Required]
		public int BranchID { get; set; }

        [Required]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Student Name is Required")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Student Mobile No is Required")]
        public string MobileNoStudent { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Father Mobile No is Required")]
        public string MobileNoFather { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Birth Date is Required")]
        public DateTime BitrhDate { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        public int Age { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

		public DateTime Created { get; set; }

		public DateTime Modified { get; set; }
	}
}

