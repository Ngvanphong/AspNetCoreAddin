using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.Data.ViewModels.Indentity;
using AspNetCoreAddin.Utilities.Constants;
using AspNetCoreAddin.Utilities.Dtos;
using AspNetCoreAddin.WebApi.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAddin.WebApi.Controllers
{
    
    public class AppRoleController : ApiController
    {
        private RoleManager<AppRole> _roleManager;
        private IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public AppRoleController(RoleManager<AppRole> roleManger,IMapper mapper,IAuthorizationService authorizationService)
        {
            _roleManager = roleManger;
            _mapper = mapper;
            _authorizationService = authorizationService;

        }
        [HttpGet]
        [Route("getlistall")]
        public IActionResult Get()
        {
            List<AppRole> listRoles = _roleManager.Roles.ToList();
            return new OkObjectResult(_mapper.Map<List<AppRoleViewModel>>(listRoles));
        }

        [HttpGet]
        [Route("getlistpaging")]
        public async Task<IActionResult> Get(int pageSize, int page = 1, string filter = null)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ROLE", Operations.Read);
            if (!hasPermission.Succeeded)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            int totalRows = 0;
            var listRole = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                listRole = listRole.Where(x => x.Description.Contains(filter) || x.Name.Contains(filter));
            }
            totalRows = listRole.Count();
            List<AppRoleViewModel> listRoleVm = _mapper.Map<List<AppRoleViewModel>>(listRole.ToList());
            return new OkObjectResult(new ApiResultPaging<AppRoleViewModel>()
            {
                PageIndex = page,
                PageSize = pageSize,
                Items = listRoleVm,
                TotalRows = totalRows
            }) ;
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            return new OkObjectResult(_mapper.Map<AppRoleViewModel>(appRole));
        }



    }
}
