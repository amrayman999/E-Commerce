using System.ComponentModel.DataAnnotations;

namespace AdminDashboardMVC.Models
{
	public class RoleViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsSelected { get; set; }
	}
}
