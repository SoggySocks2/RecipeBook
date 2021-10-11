using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccounts.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountProxy _proxy;

        public UserAccountsController(IUserAccountProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Get a list of all customers
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all user accounts", Description = "Get all active user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponse<List<ExistingUserAccountModel>>>> GetListAsync([FromQuery] PaginationFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await _proxy.GetListAsync(filter, cancellationToken);
            return Ok(result);
        }
    }
}
