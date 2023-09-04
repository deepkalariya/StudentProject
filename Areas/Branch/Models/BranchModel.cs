using System;
using System.ComponentModel.DataAnnotations;
namespace StudentProject.Areas.Branch.Models
{
	public class BranchModel
	{
		public BranchModel()
		{
		}

		public int? BranchID { get; set; }

		[Required(ErrorMessage = "Branch Name is Required")]
		public string BranchName { get; set; }

		[Required(ErrorMessage = "Branch Code is Required")]
		public string BranchCode { get; set; }

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Modified { get; set; }
	}
}

