﻿using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.IReponsitories;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AspNetCoreAddin.Utilities.Constants;
using AspNetCoreAddin.Utilities.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAddin.Application.Implementations
{
    public class ProductService : IProductService
    {
        private IMapper _mapper;
        private ITagRepository _tagRepository;
        private IRepository<Product, int> _productRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<ProductTag, int> _productTagRepository;

        public ProductService(IMapper mapper, ITagRepository tagRepository, IRepository<Product, int> productRepository, IUnitOfWork unitOfWork,
            IRepository<ProductTag, int> productTagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ProductViewModel> AddAsync(ProductViewModel productVm)
        {
            Product product = _mapper.Map<Product>(productVm);
            await _productRepository.AddAsync(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tag))
            {
                string[] listTag = product.Tag.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagRepository.FindById(tagId) == null)
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = listTag[i],
                            Type = CommonConstants.ProductTag,
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = product.Id,
                        TagId = tagId,
                    };
                    _productTagRepository.Add(productTag);
                }
            }
            return productVm;
        }

        public void Update(ProductViewModel productVm)
        {
            Product product = _mapper.Map<Product>(productVm);
            _productRepository.Update(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tag))
            {
                DeleteProductTagByProductId(product.Id);
                _unitOfWork.Commit();
                string[] listTag = product.Tag.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagRepository.FindById(tagId) == null)
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = listTag[i],
                            Type = CommonConstants.ProductTag,
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = product.Id,
                        TagId = tagId,
                    };
                    _productTagRepository.Add(productTag);
                }
            }
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public List<string> GetProductName(string productName)
        {
            var listProduct = _productRepository.FindAll(x => x.Name.Contains(productName));
            List<string> listNames = new List<string>();
            foreach (var item in listProduct)
            {
                listNames.Add(item.Name);
            }
            return listNames;
        }

        public List<ProductViewModel> GetAll()
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll().OrderByDescending(x => x.DateCreated).ToList());
        }

        public List<ProductViewModel> GetAll(int? categoryId, string hotPromotion, string keyword, int page, int pageSize, out int totalRow)
        {
            var listProduct = _productRepository.FindAll(c => c.ProductCategory);
            if (categoryId.HasValue)
            {
                listProduct = listProduct.Where(x => x.CategoryId == categoryId);
            }
            if (hotPromotion == "Hot")
            {
                listProduct = listProduct.Where(x => x.HotFlag == true);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                bool flagId = int.TryParse(keyword, out int id);
                if (flagId == true)
                {
                    listProduct = listProduct.Where(x => x.Id == id);
                }
                else
                {
                    listProduct = listProduct.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
                }
            }
            totalRow = listProduct.Count();
            listProduct = listProduct.OrderByDescending(d => d.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(listProduct.ToList());
        }

        public List<ProductViewModel> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            var listProduct = _productRepository.FindAll().OrderByDescending(x => x.DateCreated);
            totalRow = listProduct.Count();
            return _mapper.Map<List<ProductViewModel>>(listProduct.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public ProductViewModel GetById(int id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.FindById(id, c => c.ProductCategory));
        }

        public List<ProductViewModel> GetHotProductWithNumber(int number)
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll(x => x.HotFlag == true && x.Status == Data.Enums.Status.Active)
                .OrderByDescending(x => x.DateModified).Take(number).ToList());
        }

        public List<ProductViewModel> GetAllHotProduct(int page, int pageSize, out int totalRow)
        {
            var listHotProduct = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateModified);
            totalRow = listHotProduct.Count();
            return _mapper.Map<List<ProductViewModel>>(listHotProduct.Take((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public List<ProductViewModel> GetAllByTagPaging(string tag, int page, int pageSize, string sort, out int totalRow)
        {
            var products = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active);
            var productTags = _productTagRepository.FindAll();
            var query = from p in products
                        join pt in productTags on p.Id equals pt.ProductId
                        where pt.TagId == tag
                        select p;
            switch (sort)
            {
                case "nameIncrease":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "nameDecrease":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "priceIncrease":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceDecrease":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateModified);
                    break;
            }
            totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(query.ToList());
        }

        public List<ProductViewModel> GetAllByCategoryPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.CategoryId == categoryId, c => c.ProductCategory);
            switch (sort)
            {
                case "nameIncrease":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "nameDecrease":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "priceIncrease":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceDecrease":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateModified);
                    break;
            }
            totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(query.ToList());
        }

        public List<ProductViewModel> GetAllByNamePaging(string Name, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.Name.Contains(Name));
            totalRow = query.Count();
            query = query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(query.ToList());
        }

        public List<ProductViewModel> GetProductRelate(int categoryId, int number)
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll(x => x.CategoryId == categoryId && x.Status == Data.Enums.Status.Active)
                .OrderByDescending(x => x.DateModified).Take(number).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateDb(Product productDb)
        {
            _productRepository.Update(productDb);
        }

        public Product GetProductDbById(int id)
        {
            return _productRepository.FindById(id);
        }

        private void DeleteProductTagByProductId(int productId)
        {
            List<ProductTag> listProductTags = _productTagRepository.FindAll(x => x.ProductId == productId).ToList();
            _productTagRepository.RemoveMultiple(listProductTags);
        }

        public List<ProductViewModel> GetNewProductWithNumber(int number)
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll(x => x.Status == Data.Enums.Status.Active)
                .OrderByDescending(x => x.DateCreated).Take(number).ToList());
        }

        public List<TagViewModel> GetAllTag(int number)
        {
            return _mapper.Map<List<TagViewModel>>(_tagRepository.FindAll(x => x.Type == CommonConstants.ProductTag)
                .OrderByDescending(x => x.Id).Take(number).ToList());
        }

        public List<TagViewModel> GetTagByProductId(int productId)
        {
            return _mapper.Map<List<TagViewModel>>(_tagRepository.GetTagByProductId(productId));
        }

        public TagViewModel GetTagById(string id)
        {
            return _mapper.Map<TagViewModel>(_tagRepository.FindById(id));
        }
    }
}