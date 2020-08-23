using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.Enums;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreAddin.Application.Implementations
{
   public class ProductCategoryService:IProductCategoryService
    {
        private IRepository<ProductCategory, int> _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductCategoryService(IRepository<ProductCategory, int> productCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            List<ProductCategoryViewModel> lisProductCategoryVm = _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll().ToList());
            return lisProductCategoryVm;
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var productCategory = _productCategoryRepository.FindAll(x => x.Name.Contains(keyword) || x.Description.Contains(keyword)).ToList();
                return _mapper.Map<List<ProductCategoryViewModel>>(productCategory);
            }
            else
            {
                return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll().ToList());
            }
        }


        public ProductCategoryViewModel GetById(int id)
        {
            return _mapper.Map<ProductCategoryViewModel>(_productCategoryRepository.FindById(id));
        }

        public ProductCategory GetByIdDb(int id)
        {
            return _productCategoryRepository.FindById(id);
        }

        public List<ProductCategoryViewModel> GetCategoryFooter(int top)
        {
            var all = _productCategoryRepository.FindAll(x => x.Status == Status.Active).OrderBy(x => x.Name).Take(top);
            return _mapper.Map<List<ProductCategoryViewModel>>(all.ToList());
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll(x => x.Status == Status.Active, c => c.Products)
                 .OrderByDescending(x => x.HomeOrder).Take(top).ToList());
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategogy = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Update(productCategogy);
        }

        public void UpdateDb(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }
    }
}
