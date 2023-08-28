using System;
namespace StudentProject.Areas.Branch.Models
{
	public class BranchModel
	{
		public BranchModel()
		{
		}

		public int? BranchID { get; set; }

		public string BranchName { get; set; }

		public string BranchCode { get; set; }

		public DateTime Created { get; set; }

		public DateTime Modified { get; set; }
	}
}

