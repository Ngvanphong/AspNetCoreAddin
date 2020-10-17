using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAddin.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
   // [Authorize]
    public class ApiController : ControllerBase
    {

    }
}
