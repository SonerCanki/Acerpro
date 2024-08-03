using Acerpro.Common.DTOs.User;

namespace Acerpro.Common.Model
{
    public interface IWorkContext
    {
        UserResponse CurrentUser { get; set; }
    }
}
