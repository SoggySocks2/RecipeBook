using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccountProxy _proxy;

        public AuthenticationController(IUserAccountProxy proxy)
        {
            _proxy = proxy;

        }

        /// <summary>
        /// Authenticate and generate a new JWT
        /// </summary>
        /// <returns>JWT</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get JWT Token", Description = "Authenticate and generate a jason web token", Tags = new[] { "JWT" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetTokenAsync([FromBody] AuthenticationModel authenticationModel, CancellationToken cancellationToken = default)
        {
            if (authenticationModel == null) return BadRequest($"{nameof(authenticationModel)} is required");

            var tokenString = await _proxy.AuthenticateAsync(authenticationModel, cancellationToken);

            return Ok(new { Token = tokenString });
        }
    }
}
