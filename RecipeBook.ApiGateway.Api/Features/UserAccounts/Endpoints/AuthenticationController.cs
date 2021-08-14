using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccountProxy _proxy;
        private readonly ILogWriter _logWriter;

        public AuthenticationController(IUserAccountProxy proxy, ILogWriter logWriter)
        {
            _proxy = proxy;
            _logWriter = logWriter;

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
        public async Task<ActionResult> GetTokenAsync([FromBody] AuthenticationModel authenticationModel, CancellationToken cancellationToken = default)
        {
            if (authenticationModel == null) return BadRequest($"{nameof(authenticationModel)} is required");

            try
            {
                var tokenString = await _proxy.AuthenticateAsync(authenticationModel, cancellationToken);

                //TODO: log error asynchronously when an appropriate method exists
                _logWriter.LogInformation($"{authenticationModel.Username} authenticated");
                return Ok(new { Token = tokenString });
            }
            catch (EmptyInputException ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logWriter.LogError($"Attempting to authenticate {authenticationModel.Username} failed: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (AuthenticateException ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logWriter.LogError($"Attempting to authenticate {authenticationModel.Username} failed: {ex.Message}");
                return Unauthorized("Authentication failed");
            }
            catch (Exception ex)
            {
                //TODO: log error asynchronously when an appropriate method exists
                _logWriter.LogError($"Attempting to authenticate {authenticationModel.Username} failed: {ex.Message}");
                return BadRequest("Something went wrong. Please contact your administrator");
            }
        }
    }
}
