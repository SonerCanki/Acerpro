using Acerpro.Core.Entity;

namespace Acerpro.Model.Entities
{
    public class Employee : CoreEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
