using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using AspNetCoreAddin.Utilities.Constants;
using AspNetCoreAddin.Utilities.Dtos;
using AspNetCoreAddin.WebApi.Authorization;
using AspNetCoreAddin.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Controllers
{
    public class AppUserController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private IAppUserService _appUserService;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserController(UserManager<AppUser> userManager, IMapper mapper, IWebHostEnvironment env, IAppUserService appUserService,
            IAuthorizationService authorizationService, SignInManager<AppUser> signInManager)
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
                PageIndex = page,
                PageSize = pageSize,
                TotalRows = totalRows,
                Items = listAppUserVm
            });
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (!hasPermission.Succeeded)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            var listRole = await _userManager.GetRolesAsync(appUser);
            AppUserViewModel appUserVm = _mapper.Map<AppUserViewModel>(appUser);
            appUserVm.Roles = listRole;
            return new OkObjectResult(appUserVm);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AppUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Create);
                    if (!hasPermission.Succeeded)
                    {
                        return new BadRequestObjectResult(CommonConstants.Forbidden);
                    }
                    AppUser appUser = _mapper.Map<AppUser>(appUserVm);
                    var result = await _userManager.CreateAsync(appUser, appUserVm.Password);
                    if (result.Succeeded)
                    {
                        var roles = appUserVm.Roles.ToArray();
                        await _userManager.AddToRolesAsync(appUser, roles);
                        return new OkObjectResult(appUserVm);
                    }
                }catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] AppUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Update);
                    if (!hasPermission.Succeeded)
                    {
                        return new BadRequestObjectResult(CommonConstants.Forbidden);
                    }
                    AppUser appUser = await _userManager.FindByIdAsync(appUserVm.Id.ToString());
                    var roles = await _userManager.GetRolesAsync(appUser);
                    await _appUserService.RemoveRolesFromUserCustom(appUser.Id.ToString(), roles.ToArray());
                    string oldPath = appUser.Avatar;
                    if (oldPath != appUserVm.Avatar && !string.IsNullOrEmpty(oldPath))
                    {
                        oldPath.DeleteElementByString(_env);
                    }
                    appUser.UpdateUser(appUserVm);
                    appUser.SecurityStamp = Guid.NewGuid().ToString();
                    await _userManager.UpdateAsync(appUser);
                    var newRoles = appUserVm.Roles.ToArray();
                    newRoles = newRoles ?? new string[] { };
                    await _userManager.AddToRolesAsync(appUser, newRoles);
                    return new OkObjectResult(appUserVm);
                }catch(Exception e)
                {
                    return new BadRequestObjectResult(e.Message);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }
    }
}