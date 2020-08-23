using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.EF;
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
    public class AboutService : IAboutService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepository<About, int> _aboutRepository;
        public AboutService(IUnitOfWork unitOfWork,IMapper mapper,IRepository<About,int> aboutRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aboutRepository = aboutRepository;
        }
        public void Add(About contact)
        {
            _aboutRepository.Add(contact);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public AboutViewModel GetAbout()
        {
            return _mapper.Map<AboutViewModel>(_aboutRepository.FindAll().SingleOrDefault());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(About contact)
        {
            _aboutRepository.Update(contact);
        }
    }
}
