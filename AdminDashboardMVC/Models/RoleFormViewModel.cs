using System.ComponentModel.DataAnnotations;

namespace AdminDashboardMVC.Models
{
	public class RoleFormViewModel
	{
		[Required(ErrorMessage ="Name is required")]
		[StringLength(256)]
		public string Name { get; set; }
	}
}
