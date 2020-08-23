using AspNetCoreAddin.Application.Implementations;
using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.EF;
using AspNetCoreAddin.Data.EF.Reponsitories;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.IReponsitories;
using AspNetCoreAddin.Helper;
using AspNetCoreAddin.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AspNetCoreAddin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("AspNetCoreAddin.Data.EF")));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // Indentity
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            //Config Indentity
            services.Configure<IdentityOptions>(option =>
            {
                //password setting
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                //lock setting
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1440);
                option.Lockout.MaxFailedAccessAttempts = 10;
                // check had email
                option.User.RequireUniqueEmail = true;
            });

            // Seek database
            services.AddTransient<DbInitializer>();

            //Automapper
            services.AddAutoMapper(typeof(Startup));

            //UnitOfWork
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            //Cliam
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactoryApi>();

            //Repository
            services.AddTransient<IRepository<ProductCategory, int>, EFRepository<ProductCategory, int>>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IRepository<Product, int>, EFRepository<Product, int>>();
            services.AddTransient<IRepository<ProductTag, int>, EFRepository<ProductTag, int>>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRepository<ProductImage, int>, EFRepository<ProductImage, int>>();
            services.AddTransient<IRepository<Blog, int>, EFRepository<Blog, int>>();
            services.AddTransient<IRepository<BlogTag, int>, EFRepository<BlogTag, int>>();
            services.AddTransient<IRepository<BlogImage, int>, EFRepository<BlogImage, int>>();
            services.AddTransient<IRepository<Slide, int>, EFRepository<Slide, int>>();
            services.AddTransient<IRepository<Data.Entities.Contact, string>, EFRepository<Data.Entities.Contact, string>>();
            services.AddTransient<IRepository<Subcrible, int>, EFRepository<Subcrible, int>>();
            services.AddTransient<IRepository<SystemConfig, string>, EFRepository<SystemConfig, string>>();
            services.AddTransient<IRepository<About, int>, EFRepository<About, int>>();
            services.AddTransient<IRepository<Comment, int>, EFRepository<Comment, int>>();

            // Service
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IBlogImageService, BlogImageService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ISubcribleService, SubcribleService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ISystemConfigService, SystemConfigService>();
            services.AddTransient<IAboutService, AboutService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}