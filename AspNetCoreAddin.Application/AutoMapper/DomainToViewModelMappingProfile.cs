using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.AutoMapper
{
   public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<BlogTag, BlogTagViewModel>();
            CreateMap<Blog, BlogViewModel>();
            CreateMap<BlogImage, BlogImageViewModel>();
            CreateMap<Slide, SlideViewModel>();
            CreateMap<Contact, ContactViewModel>();
            CreateMap<Subcrible, SubcribleViewModel>();
            CreateMap<SystemConfig, SystemConfigViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<About, AboutViewModel>();

        }
    }
}
