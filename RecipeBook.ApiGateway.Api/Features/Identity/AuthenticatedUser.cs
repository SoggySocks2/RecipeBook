using Microsoft.AspNetCore.Http;
using RecipeBook.SharedKernel.Contracts;
using System;
using System.Security.Claims;

namespace RecipeBook.ApiGateway.Api.Features.Identity
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        public Guid Id { get; private set;  }
        public string Name { get; private set; }

        public AuthenticatedUser()
        {
            Id = Guid.Empty;
            Name = string.Empty;
        }

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                Id = Guid.Empty;
                Name = string.Empty;
                return;
            }

            var user = httpContextAccessor.HttpContext?.User;
            if (user is null)
            {
                Id = Guid.Empty;
                Name = string.Empty;
                return;
            }

            var nameIdenfier = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nameIdenfier is null)
            {
                Id = Guid.Empty;
                Name = string.Empty;
                return;
            }

            Id = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            Name = user.FindFirstValue(ClaimTypes.Name);
        }
    }
}
