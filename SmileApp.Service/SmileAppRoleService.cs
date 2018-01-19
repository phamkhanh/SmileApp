using System.Collections.Generic;
using System.Linq;
using SmileApp.Common.Exceptions;
using SmileApp.Data.Infrastructure;
using SmileApp.Data.Repositories;
using SmileApp.Model.Models;

namespace SmileApp.Service
{
    public interface ISmileAppRoleService
    {
        SmileAppRole GetDetail(string id);

        IEnumerable<SmileAppRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<SmileAppRole> GetAll();

        SmileAppRole Add(SmileAppRole appRole);

        void Update(SmileAppRole AppRole);

        void Delete(string id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<SmileAppRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<SmileAppRole> GetListRoleByGroupId(int groupId);

        void Save();
    }

    public class SmileAppRoleService : ISmileAppRoleService
    {
        private ISmileAppRoleRepository _appRoleRepository;
        private ISmileAppRoleGroupRepository _appRoleGroupRepository;
        private IUnitOfWork _unitOfWork;

        public SmileAppRoleService(IUnitOfWork unitOfWork,
            ISmileAppRoleRepository appRoleRepository, ISmileAppRoleGroupRepository appRoleGroupRepository)
        {
            this._appRoleRepository = appRoleRepository;
            this._appRoleGroupRepository = appRoleGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public SmileAppRole Add(SmileAppRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<SmileAppRoleGroup> roleGroups, int groupId)
        {
            _appRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                _appRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public void Delete(string id)
        {
            _appRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<SmileAppRole> GetAll()
        {
            return _appRoleRepository.GetAll();
        }

        public IEnumerable<SmileAppRole> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public SmileAppRole GetDetail(string id)
        {
            return _appRoleRepository.GetSingleByCondition(x => x.Id == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(SmileAppRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appRoleRepository.Update(AppRole);
        }

        public IEnumerable<SmileAppRole> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepository.GetListRoleByGroupId(groupId);
        }
    }
}