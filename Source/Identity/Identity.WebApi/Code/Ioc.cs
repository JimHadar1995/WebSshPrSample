using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Identity.Application.Internal;
using Identity.Application.Services.Implementations;
using Identity.Infrastructure.Code;
using Identity.WebApi.Code.Filters;
using Library.Common.Authentication;
using Library.Common.Jaeger;
using Library.Common.Kafka;
using Library.Logging.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Identity.WebApi.Code
{
    /// <summary>
    /// Di
    /// </summary>
    internal static class Ioc
    {
        internal static void InitializeDiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureInfrastructureServices();
            services.ConfigureAppServices();
            services.Configure<KafkaConnection>(configuration.GetSection(nameof(KafkaConnection)));
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opt.JsonSerializerOptions.IgnoreNullValues = false;
                    opt.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthService>());

            services.ConfigureSCLogger();
            services.ConfigureSwagger();

            services.AddJwt();

            //services.InitializeData().Wait();

            //services.AddHostedService<IdentityHostedService>();

            services.AddOpenTracing();
            services.AddJaeger();
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type t in a.GetTypes())
                    {
                        if (t.IsEnum)
                        {
                            c.MapType(t, () => new OpenApiSchema
                            {
                                Type = "string",
                                Enum = t.GetEnumNames().Select(name => new OpenApiString(name)).Cast<IOpenApiAny>().ToList(),
                                Nullable = true
                            });
                        }
                    }
                }

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity service API",
                    Version = "v1",
                    Description = "API для работы с ядром Identity",
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = "test@mail.ru",
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
    }
}
