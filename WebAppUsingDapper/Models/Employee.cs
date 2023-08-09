using System.Text.Json.Serialization;
using WebAppUsingDapper.Models;

namespace WebAppUsingDapper.Models
{
    public partial class Employee : BaseEntity
    {
		public Employee()
		{
			Dept = null!;
		}
        public int Age { get; set; }
        public decimal? Salary { get; set; }
        public int DeptId { get; set; }
        [JsonIgnore]
        public virtual Department Dept { get; set; }
    }
}
