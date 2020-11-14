using AspNetCoreAddin.Application.Interfaces;
using AspNetCoreAddin.Data.Entities;
using AspNetCoreAddin.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAddin.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IPermissionService _permissionService;
        private readonly ILogger _logger;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IConfiguration config, IPermissionService permissionService, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _permissionService = permissionService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
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
                var props = new Dictionary<string, string>
                {
                    {"fullName",user.FullName },
                    {"avatar",user.Avatar },
                    {"email", user.Email },
                    {"username", user.UserName },
                    { "permissions",JsonConvert.SerializeObject(permissionViewModels) },
                    {"role",JsonConvert.SerializeObject(roles) }
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_config["Token:Issuer"],
                    _config["Token:Issuer"],
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddDays(28),
                    signingCredentials: creds
                    );
                _logger.LogInformation(1, "User logged in.");
                return new OkObjectResult(new { token = new JwtSecurityTokenHandler().WriteToken(token), userLogin = props });
            }
            return new BadRequestObjectResult("Login failure");
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(model);
            }
            var user = new AppUser { FullName = "denmo", UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password); ;
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation(3, "User created a new account with password.");
                return new OkObjectResult(model);
            }
            return new BadRequestObjectResult(model);
        }
    }
}