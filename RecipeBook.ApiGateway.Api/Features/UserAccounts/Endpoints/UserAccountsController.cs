using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Models;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
        private readonly ILogWriter _logWriter;

        public UserAccountsController(IUserAccountProxy proxy, ILogWriter logWriter)
        {
            _proxy = proxy;
            _logWriter = logWriter;
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
            try
            {
                var result = await _proxy.GetListAsync(filter, cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == cancellationToken)
            {
                _logWriter.LogInformation("Operation cancelled: " + ex.Message);
                return BadRequest("Operation cancelled: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logWriter.LogError("System error: " + ex.Message);
                return BadRequest("System error");
            }
        }
    }
}
