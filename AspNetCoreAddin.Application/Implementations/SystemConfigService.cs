using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreAddin.Application.Implementations
{
   public class SystemConfigService:ISystemConfigService
    {
        private IRepository<SystemConfig, string> _systemConfigRepository;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public SystemConfigService(IRepository<SystemConfig, string> systemConfigRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _systemConfigRepository = systemConfigRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public void Add(SystemConfigViewModel systemConfig)
        {
            _systemConfigRepository.Add(_mapper.Map<SystemConfig>(systemConfig));
        }

        public void Delete(string id)
        {
            _systemConfigRepository.Remove(id);
        }

        public SystemConfigViewModel Detail(string id)
        {
            return _mapper.Map<SystemConfigViewModel>(_systemConfigRepository.FindById(id));
        }

        public SystemConfig DetailDb(string id)
        {
            return _systemConfigRepository.FindById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SystemConfigViewModel> GetAll()
        {
            return _mapper.Map<List<SystemConfigViewModel>>(_systemConfigRepository.FindAll().ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SystemConfig systemConfigDb)
        {
            _systemConfigRepository.Update(systemConfigDb);
        }
    }
}
