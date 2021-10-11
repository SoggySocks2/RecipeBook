using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountProxy _proxy;

        public UserAccountController(IUserAccountProxy proxy)
        {
            _proxy = proxy;

        }

        /// <summary>
        /// Add a new user account
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create user account", Description = "Create a new user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExistingUserAccountModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExistingUserAccountModel>> AddAsync([FromBody] NewUserAccountModel userAccount, CancellationToken cancellationToken = default)
        {
            if (userAccount == null) return BadRequest($"{nameof(userAccount)} is required");

            var result = await _proxy.AddAsync(userAccount, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get a user account
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user account", Description = "Get a user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExistingUserAccountModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExistingUserAccountModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return BadRequest($"{nameof(id)} is required");

            var result = await _proxy.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing user account
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update user account", Description = "Update an existing user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExistingUserAccountModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ExistingUserAccountModel userAccount, CancellationToken cancellationToken = default)
        {
            if (userAccount?.Id != id) return BadRequest($"{(nameof(id))} is invalid");

            var result = await _proxy.UpdateAsync(userAccount, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete an existing user account
        /// </summary>
        /// <param name="id">User account id</param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete user account", Description = "Delete an existing user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return BadRequest($"{nameof(id)} is required");

            await _proxy.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
    }
}