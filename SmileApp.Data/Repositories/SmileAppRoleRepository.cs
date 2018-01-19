using System.Collections.Generic;
using System.Linq;
using SmileApp.Data.Infrastructure;
using SmileApp.Model.Models;

namespace SmileApp.Data.Repositories
{
    public interface ISmileAppRoleRepository : IRepository<SmileAppRole>
    {
        IEnumerable<SmileAppRole> GetListRoleByGroupId(int groupId);
    }

    public class SmileAppRoleRepository : RepositoryBase<SmileAppRole>, ISmileAppRoleRepository
    {
        public SmileAppRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<SmileAppRole> GetListRoleByGroupId(int groupId)
        {
            var query = from g in DbContext.SmileAppRoles
                        join ug in DbContext.SmileAppRoleGroups
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query;
        }
    }
}