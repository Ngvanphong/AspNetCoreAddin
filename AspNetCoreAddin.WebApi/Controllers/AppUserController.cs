using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using AspNetCoreAddin.Utilities.Constants;
using AspNetCoreAddin.Utilities.Dtos;
using AspNetCoreAddin.WebApi.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAddin.WebApi.Controllers
{
    
    public class AppUserController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private IHostingEnvironment _env;
        private IAppUserService _appUserService;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<AppUser> _signInManager;
        public AppUserController(UserManager<AppUser> userManager,IMapper mapper, IHostingEnvironment env, IAppUserService appUserService,
            IAuthorizationService authorizationService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _appUserService = appUserService;
            _authorizationService = authorizationService;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public async Task<IActionResult> Get(int pageSize, int page, string filter = null)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (!hasPermission.Succeeded)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            var listUser = _userManager.Users;
            int totalRows = 0;
            if (!string.IsNullOrEmpty(filter))
            {
                listUser = listUser.Where(x => x.Id.ToString() == filter || x.UserName.Contains(filter) || x.FullName.Contains(filter));
            }
            totalRows = listUser.Count();
            listUser = listUser.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            List<AppUserViewModel> listAppUserVm = _mapper.Map<List<AppUserViewModel>>(listUser.ToList());
            return new OkObjectResult(new ApiResultPaging<AppUserViewModel>()
            {
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRows,
                Items=listAppUserVm
            });            
        } 

    }
}
