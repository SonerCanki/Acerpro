using Acerpro.Common.Enums;
using Acerpro.UI.Models.DepartmentViewModels;

namespace Acerpro.UI.Models.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid? DepartmentId { get; set; }

        public DepartmentViewModel Department { get; set; }
    }
}
