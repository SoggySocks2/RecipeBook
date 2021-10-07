using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecipeBook.ApiGateway.Api.Configuration;
using RecipeBook.ApiGateway.Api.Features.Identity;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Contracts;
using RecipeBook.ApiGateway.Api.Features.UserAccounts.Proxies;
using RecipeBook.CoreApp.Api.Configuration;
using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.SharedObjects;

namespace RecipeBook.ApiGateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* Load client configuration settings */
            Configuration.Bind(ClientSettings.CONFIG_NAME, ClientSettings.Instance);

            /* Set pages response properties to configuration values */
            PaginationHelper.SetDefaults(ClientSettings.Instance.DefaultPageSize, ClientSettings.Instance.DefaultPageSizeLimit);

            services.AddSingleton<IClientSettings>(services => ClientSettings.Instance);
            services.AddJwtAuthentication(Configuration);
            services.AddScoped<IUserAccountProxy, UserAccountProxy>();
            services.AddCoreAppServices(Configuration);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                    options.ImplicitlyValidateRootCollectionElements = true;
                });

            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

            /* Enable swagger documentation */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecipeBook.ApiGateway.Api", Version = "v1" });
            });

            /*
                Ensure all endpoints require authentication. Use [AllowAnonymous] on endpoints
                that don't require authentication, such as GetTokenAsync()
            */
            services.AddMvcCore(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole("Admin") /* Restrict access to endpoints to the Admin role */
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook.ApiGateway.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
