using Acerpro.Model.Context;
using Acerpro.Service.Repository.Base;
using E = Acerpro.Model.Entities;


namespace Acerpro.Service.Repository.Department
{
    public class DepartmentRepository : Repository<E.Department>, IDepartmentRepository
    {
        private readonly DataContext _dataContext;
        public DepartmentRepository(DataContext context)
            : base(context)
        {
            _dataContext = context;
        }
    }
}
