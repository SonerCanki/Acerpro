using Acerpro.Core.Entity;

namespace Acerpro.Model.Entities
{
    public class Department : CoreEntity
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
