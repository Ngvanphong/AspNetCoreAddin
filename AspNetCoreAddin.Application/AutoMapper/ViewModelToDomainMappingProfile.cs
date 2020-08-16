using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Application.AutoMapper
{
   public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c));
            CreateMap<FunctionViewModel, Function>()
               .ConstructUsing(c => new Function(c));
            CreateMap<ProductViewModel, Product>()
               .ConstructUsing(c => new Product(c));
            CreateMap<ProductImageViewModel, ProductImage>()
              .ConstructUsing(c => new ProductImage(c));
            CreateMap<AppRoleViewModel, AppRole>()
            .ConstructUsing(c => new AppRole(c.Name, c.Description));
            CreateMap<AppUserViewModel, AppUser>()
             .ConstructUsing(c => new AppUser(c));
            CreateMap<PermissionViewModel, Permission>()
             .ConstructUsing(c => new Permission(c));
            CreateMap<BlogViewModel, Blog>()
            .ConstructUsing(c => new Blog(c));
            CreateMap<TagViewModel, Tag>()
           .ConstructUsing(c => new Tag(c));
            CreateMap<BlogTagViewModel, BlogTag>()
           .ConstructUsing(c => new BlogTag(c));
            CreateMap<BlogImageViewModel, BlogImage>()
            .ConstructUsing(c => new BlogImage(c));
            CreateMap<SlideViewModel, Slide>()
             .ConstructUsing(c => new Slide(c));
            CreateMap<ContactViewModel, Contact>()
           .ConstructUsing(c => new Contact(c));
            CreateMap<SubcribleViewModel, Subcrible>()
          .ConstructUsing(c => new Subcrible(c));
            CreateMap<SystemConfigViewModel, SystemConfig>()
         .ConstructUsing(c => new SystemConfig(c));
            CreateMap<AboutViewModel, About>()
         .ConstructUsing(c => new About(c));
            CreateMap<CommentViewModel, Comment>()
         .ConstructUsing(c => new Comment(c));
        }
    }
}
