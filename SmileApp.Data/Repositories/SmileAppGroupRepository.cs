using System.Collections.Generic;
using System.Linq;
using SmileApp.Data.Infrastructure;
using SmileApp.Model.Models;

namespace SmileApp.Data.Repositories
{
    public interface ISmileAppGroupRepository : IRepository<SmileAppGroup>
    {
        IEnumerable<SmileAppGroup> GetListGroupByUserId(string userId);

        IEnumerable<SmileAppUser> GetListUserByGroupId(int groupId);
    }

    public class SmileAppGroupRepository : RepositoryBase<SmileAppGroup>, ISmileAppGroupRepository
    {
        public SmileAppGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<SmileAppGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.SmileAppGroups
                        join ug in DbContext.SmileAppUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }

        public IEnumerable<SmileAppUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.SmileAppGroups
                        join ug in DbContext.SmileAppUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query;
        }
    }
}