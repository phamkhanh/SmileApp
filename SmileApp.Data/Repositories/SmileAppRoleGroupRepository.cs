using SmileApp.Data.Infrastructure;
using SmileApp.Model.Models;

namespace SmileApp.Data.Repositories
{
    public interface ISmileAppRoleGroupRepository : IRepository<SmileAppRoleGroup>
    {
    }

    public class SmileAppRoleGroupRepository : RepositoryBase<SmileAppRoleGroup>, ISmileAppRoleGroupRepository
    {
        public SmileAppRoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}