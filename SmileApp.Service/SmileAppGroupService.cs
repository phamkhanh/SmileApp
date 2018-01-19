using System.Collections.Generic;
using System.Linq;
using SmileApp.Common.Exceptions;
using SmileApp.Data.Infrastructure;
using SmileApp.Data.Repositories;
using SmileApp.Model.Models;

namespace SmileApp.Service
{
    public interface ISmileAppGroupService
    {
        SmileAppGroup GetDetail(int id);

        IEnumerable<SmileAppGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<SmileAppGroup> GetAll();

        SmileAppGroup Add(SmileAppGroup appGroup);

        void Update(SmileAppGroup appGroup);

        SmileAppGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<SmileAppUserGroup> groups, string userId);

        IEnumerable<SmileAppGroup> GetListGroupByUserId(string userId);

        IEnumerable<SmileAppUser> GetListUserByGroupId(int groupId);

        void Save();
    }

    public class SmileAppGroupService : ISmileAppGroupService
    {
        private ISmileAppGroupRepository _appGroupRepository;
        private IUnitOfWork _unitOfWork;
        private ISmileAppUserGroupRepository _appUserGroupRepository;

        public SmileAppGroupService(IUnitOfWork unitOfWork,
            ISmileAppUserGroupRepository appUserGroupRepository,
            ISmileAppGroupRepository appGroupRepository)
        {
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public SmileAppGroup Add(SmileAppGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appGroupRepository.Add(appGroup);
        }

        public SmileAppGroup Delete(int id)
        {
            var appGroup = this._appGroupRepository.GetSingleById(id);
            return _appGroupRepository.Delete(appGroup);
        }

        public IEnumerable<SmileAppGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public IEnumerable<SmileAppGroup> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public SmileAppGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(SmileAppGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("Tên không được trùng");
            _appGroupRepository.Update(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<SmileAppUserGroup> userGroups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public IEnumerable<SmileAppGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<SmileAppUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }
    }
}