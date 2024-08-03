using Acerpro.Common.DTOs.Employee;

namespace Acerpro.Common.DTOs.Department
{
    public class DepartmentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<EmployeeResponse> Employees { get; set; } = new();
    }
}
