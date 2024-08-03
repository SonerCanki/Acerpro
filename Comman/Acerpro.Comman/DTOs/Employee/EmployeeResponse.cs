using Acerpro.Common.DTOs.Department;

namespace Acerpro.Common.DTOs.Employee
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid DepartmentId { get; set; }

        public DepartmentResponse Department{ get; set; }
    }
}
