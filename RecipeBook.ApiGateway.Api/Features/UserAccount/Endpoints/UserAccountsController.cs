using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccount.Models;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.CustomExceptions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Features.UserAccount.Endpoints
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
        public async Task<ActionResult<List<ExistingUserAccountModel>>> GetAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _proxy.GetListAsync(cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == cancellationToken) // includes TaskCanceledException
            {
                _logWriter.LogInformation("Operation cancelled for GET UserAccounts endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (EmptyInputException ex)
            {
                _logWriter.LogWarning("Empty Input error thrown by GET UserAccounts endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (InvalidValueException ex)
            {
                _logWriter.LogWarning("Invalid Value error thrown by GET UserAccounts endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (NotFoundException ex)
            {
                _logWriter.LogWarning("Not Found error thrown by GET UserAccounts endpoint: " + ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logWriter.LogError("System error thrown by GET UserAccounts endpoint: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
