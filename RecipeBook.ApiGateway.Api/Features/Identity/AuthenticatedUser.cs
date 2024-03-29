﻿using Microsoft.AspNetCore.Http;
using RecipeBook.SharedKernel.Contracts;
using System;
using System.Security.Claims;

namespace RecipeBook.ApiGateway.Api.Features.Identity
{
    /// <summary>
    /// Represents an authenticated user
    /// </summary>
    public class AuthenticatedUser : IAuthenticatedUser
    {
        /// <summary>
        /// User account id
        /// </summary>
        public Guid Id { get; private set;  }

        /// <summary>
        /// User first name & last name
        /// </summary>
        public string Name { get; private set; }

        public AuthenticatedUser()
        {
            Id = Guid.Empty;
            Name = string.Empty;
        }

        public AuthenticatedUser(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Check for an authenticated user and set appropriate properties
        /// </summary>
        /// <param name="httpContextAccessor"></param>
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
