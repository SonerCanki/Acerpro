using Acerpro.Model.Context;
using Acerpro.Service.Repository.Base;
using E = Acerpro.Model.Entities;

namespace Acerpro.Service.Repository.User
{
    public class UserRepository : Repository<E.User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext context)
            : base(context)
        {
            _dataContext = context;
        }
    }
}
