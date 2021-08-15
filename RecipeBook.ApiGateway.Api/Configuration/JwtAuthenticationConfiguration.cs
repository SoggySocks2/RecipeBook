using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.ApiGateway.Api.Configuration
{
    public static class JwtAuthenticationConfiguration
    {
        /// <summary>
        /// Read and decrypt the incoming jason web token
        /// </summary>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            /* Read encryption key */
            //TODO: MOVE THIS KEY INTO A SECURE VAULT
            var encryptionKey = configuration.GetValue<string>("JWTEncryptionKey");
            var key = Encoding.ASCII.GetBytes(encryptionKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Validate the user
                        var name = context.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
                        if (name is null) context.Fail("UnAuthorized");

                        var roles = context.Principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                        if (roles.Count == 0) context.Fail("UnAuthorized");

                        // Set the user roles
                        var additionalClaims = new List<Claim>();
                        foreach (var role in roles)
                        {
                            additionalClaims.Add(new Claim("Role", role.Value));
                        }

                        var newIdentity = new ClaimsIdentity(context.Principal.Identity, additionalClaims, "pwd", "name", "role");
                        context.Principal = new ClaimsPrincipal(newIdentity);

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
