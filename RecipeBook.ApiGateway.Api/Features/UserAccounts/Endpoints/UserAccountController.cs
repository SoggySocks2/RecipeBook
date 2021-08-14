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
        /// Add a new user account
        /// </summary>
        /// <param name="userAccount">New user account</param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [SwaggerOperation(Summary = "Create user account", Description = "Create a new user account", Tags = new[] { "UserAccount" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExistingUserAccountModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExistingUserAccountModel>> AddAsync([FromBody] NewUserAccountModel userAccount, CancellationToken cancellationToken = default)
        {
            if (userAccount == null) return BadRequest($"{nameof(userAccount)} is required");

            try
            {
                var result = await _proxy.AddAsync(userAccount, cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == cancellationToken)
            {
                _logWriter.LogInformation("Operation cancelled: " + ex.Message);
                return BadRequest();
            }
            catch (EmptyInputException ex)
            {
                _logWriter.LogWarning("Empty Input: " + ex.Message);
                return BadRequest();
            }
            catch (InvalidValueException ex)
            {
                _logWriter.LogWarning("Invalid Value: " + ex.Message);
                return BadRequest();
            }
            catch (NotFoundException ex)
            {
                _logWriter.LogWarning("Not Found: " + ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logWriter.LogError("System error: " + ex.Message);
                return BadRequest();
            }
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
        public async Task<ActionResult<ExistingUserAccountModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return BadRequest($"{nameof(id)} is required");

            try
            {
                var result = await _proxy.GetByIdAsync(id, cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == cancellationToken)
            {
                _logWriter.LogInformation("Operation cancelled: " + ex.Message);
                return BadRequest();
            }
            catch (EmptyInputException ex)
            {
                _logWriter.LogWarning("Empty Input: " + ex.Message);
                return BadRequest();
            }
            catch (NotFoundException ex)
            {
                _logWriter.LogWarning("Not Found: " + ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logWriter.LogError("System error: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
