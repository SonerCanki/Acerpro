using Acerpro.Model.Context;
using Acerpro.Service.Repository.Base;
using E = Acerpro.Model.Entities;

namespace Acerpro.Service.Repository.Employee
{
    public class EmployeeRepository : Repository<E.Employee>, IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        public EmployeeRepository(DataContext context)
            : base(context)
        {
            _dataContext = context;
        }
    }
}
