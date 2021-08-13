using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountProxy _userAccountProxy;
        private readonly ILogWriter _logger;

        public AuthController(IUserAccountProxy userAccountProxy, ILogWriter logger)
        {
            _userAccountProxy = userAccountProxy;
            _logger = logger;
        }

        /// <summary>
        /// Authenticate and generate a new JWT
        /// </summary>
        /// <returns>JWT</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get JWT Token", Description = "Authenticate and generate a JWT", Tags = new[] { "JWT" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetTokenAsync([FromBody] AuthModel authModel, CancellationToken cancellationToken = default)
        {
            if (authModel == null)
            {
                return BadRequest("authModel is required");
            }

            try
            {
                var tokenString = await _userAccountProxy.AuthenticateAsync(authModel, cancellationToken);

                //TODO: log error asynchronously when an appropriate method exists
                _logger.LogInformation($"{authModel.Username} authenticated");
                return Ok(new { Token = tokenString });
            }
            catch (EmptyInputException ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logger.LogError($"Attempting to authenticate {authModel.Username} failed: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (AuthenticateException ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logger.LogError($"Attempting to authenticate {authModel.Username} failed: {ex.Message}");
                return Unauthorized("Authentication failed");
            }
            catch (Exception ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logger.LogError($"Attempting to authenticate {authModel.Username} failed: {ex.Message}");
                return BadRequest("Something went wrong. Please contact your administrator");
            }
        }
    }
}
