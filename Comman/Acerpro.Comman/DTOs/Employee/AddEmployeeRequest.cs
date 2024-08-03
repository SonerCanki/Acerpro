namespace Acerpro.Common.DTOs.Employee
{
    public class AddEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
