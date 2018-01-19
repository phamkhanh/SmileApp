using SmileApp.Data.Infrastructure;
using SmileApp.Model.Models;

namespace SmileApp.Data.Repositories
{
    public interface ISmileAppUserGroupRepository : IRepository<SmileAppUserGroup>
    {
    }

    public class SmileAppUserGroupRepository : RepositoryBase<SmileAppUserGroup>, ISmileAppUserGroupRepository
    {
        public SmileAppUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}