using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rescuer.Core.Base;
using Rescuer.Framework.Api;
using Rescuer.General.WebApi.Services;
using System.Net.Mime;

namespace Rescuer.General.WebApi.Controller
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<object>> Get()
        {
            var baseRespone = new BaseResponse<object>();
            baseRespone.Result = new object();
            baseRespone.StatusCode = StatusCodes.Status200OK;
            return baseRespone;
        }
    }
}