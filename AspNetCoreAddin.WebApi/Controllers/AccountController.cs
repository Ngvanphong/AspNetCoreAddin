using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IPermissionService _permissionService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IConfiguration config, IPermissionService permissionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Login(string userName, string password, bool rememberMe = false)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, true);
                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.ToString());
                }
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count() == 0)
                {
                    return new BadRequestObjectResult("Login failure");
                }
                var permissionViewModels = _permissionService.GetByUserId(user.Id);
                var claims = new[]
                {
                    new Claim("Id",user.Id.ToString()??string.Empty),
                    new Claim("fullName",user.FullName??string.Empty),
                    new Claim("avatar", user.Avatar??string.Empty),
                    new Claim("email", user.Email),
                    new Claim("username",user.UserName.ToString()),
                    new Claim("roles", string.Join(";",roles)??string.Empty),
                    new Claim("permissions", JsonConvert.SerializeObject(permissionViewModels)??string.Empty)
                };

            }
            return new BadRequestObjectResult("Login failure");
        }
    }
}