using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.Implementations
{
   public class ContactService:IContactService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Contact, string> _contactRepository;
        private IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IRepository<Contact, string> contactRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public void Add(ContactViewModel contact)
        {
            _contactRepository.Add(_mapper.Map<Contact>(contact));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ContactViewModel GetContact()
        {
            return _mapper.Map<ContactViewModel>(_contactRepository.FindSingle(x => x.Id == "Default"));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactViewModel contact)
        {
            _contactRepository.Update(_mapper.Map<Contact>(contact));
        }
    }
}
