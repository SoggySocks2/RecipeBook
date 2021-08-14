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
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountProxy _proxy;
        private readonly ILogWriter _logWriter;

        public UserAccountController(IUserAccountProxy proxy, ILogWriter logWriter)
        {
            _proxy = proxy;
            _logWriter = logWriter;

        }
        /// <summary>
        /// Get a user account
        /// </summary>
        /// <param name="id">User account id</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user account", Description = "Get a user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExistingUserAccountModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExistingUserAccountModel>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var result = await _proxy.GetByIdAsync(id, cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == cancellationToken) // includes TaskCanceledException
            {
                _logWriter.LogInformation("Operation cancelled for GET Customer endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (EmptyInputException ex)
            {
                _logWriter.LogWarning("Empty Input error thrown by GET Customer endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (NotFoundException ex)
            {
                _logWriter.LogWarning("Not Found error thrown by GET Customer endpoint: " + ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logWriter.LogError("System error thrown by GET Customer endpoint: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
