using WebAppUsingDapper.Models;

namespace WebAppUsingDapper.Models
{
    public partial class Department : BaseEntity
	{
		public Department()
		{
			Employees = new HashSet<Employee>();
		}
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
