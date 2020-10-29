using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rescuer.Core.Base;
using Rescuer.DTO.Request.Login;
using Rescuer.DTO.Response.Token;
using Rescuer.Framework.Api;
using Rescuer.Identity.WebApi.Services;
using System.Net.Mime;

namespace Rescuer.Identity.WebApi.Controllers
{
    [Route("api/login")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class LoginController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("AccessToken")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<AccessTokenDTO>> AccessToken(LoginDTO login)
        {
            var baseRespone = new BaseResponse<AccessTokenDTO>();
            if (!ModelState.IsValid)
            {
                baseRespone.ValidationMessage.Add("Model is not valid");
                baseRespone.StatusCode = StatusCodes.Status400BadRequest;
                return baseRespone;
            }

            var accessToken = _authenticationService.CreateAccessToken(login);
            if (accessToken == null)
            {
                baseRespone.ValidationMessage.Add("Access Token could not be created");
                baseRespone.StatusCode = StatusCodes.Status401Unauthorized;
                return baseRespone;
            }

            baseRespone.Result = accessToken;
            baseRespone.StatusCode = StatusCodes.Status200OK;
            return baseRespone;
        }

        [HttpPost("RefreshToken")]
        public ActionResult<BaseResponse<AccessTokenDTO>> RefreshToken(string refreshToken)
        {
            var baseRespone = new BaseResponse<AccessTokenDTO>();

            var accessToken = _authenticationService.CreateAccessTokenByRefreshToken(refreshToken);
            if (accessToken == null)
            {
                baseRespone.ValidationMessage.Add("Access Token could not be created");
                baseRespone.StatusCode = StatusCodes.Status401Unauthorized;
                return baseRespone;
            }

            baseRespone.Result = accessToken;
            baseRespone.StatusCode = StatusCodes.Status200OK;

            return baseRespone;
        }

        [HttpPost("RevokRefreshToken")]
        public ActionResult<BaseResponse<AccessTokenDTO>> RevokRefreshToken(string refreshToken)
        {
            var baseRespone = new BaseResponse<AccessTokenDTO>();
            var accessToken = _authenticationService.RevokeRefreshToken(refreshToken);
            if (accessToken == null)
            {
                baseRespone.ValidationMessage.Add("Access Token could not be created");
                baseRespone.StatusCode = StatusCodes.Status401Unauthorized;
                return baseRespone;
            }

            baseRespone.Result = accessToken;
            baseRespone.StatusCode = StatusCodes.Status200OK;

            return baseRespone;
        }
    }
}