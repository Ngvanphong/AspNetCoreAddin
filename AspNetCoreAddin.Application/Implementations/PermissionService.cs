using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.IReponsitories;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreAddin.Application.Implementations
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(PermissionViewModel permission)
        {
            _permissionRepository.Add(_mapper.Map<Permission>(permission));
        }

        public void AddDb(Permission permission)
        {
            _permissionRepository.Update(permission);
        }

        public void DeleteAll(string functionId)
        {
            List<Permission> listPermission = _permissionRepository.FindAll(x => x.FunctionId == functionId).ToList();
            _permissionRepository.RemoveMultiple(listPermission);
        }

        public void DeleteAllByRoleId(string roleId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PermissionViewModel> GetByFunctionId(string functionId)
        {
            return _mapper.Map<List<PermissionViewModel>>(_permissionRepository.FindAll(x => x.FunctionId == functionId, r => r.AppRole).ToList());
        }

        public List<PermissionViewModel> GetByUserId(Guid userId)
        {
            return _mapper.Map<List<PermissionViewModel>>(_permissionRepository.GetByUserId(userId).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}