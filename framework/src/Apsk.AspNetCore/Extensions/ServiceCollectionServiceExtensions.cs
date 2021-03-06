﻿// <copyright file="ServiceCollectionServiceExtensions.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Extensions
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Apsk.AspNetCore.AppSettings;
    using Apsk.AspNetCore.DynamicApi;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddApskRestControllers(this IServiceCollection services)
        {
            var service = services.FirstOrDefault(srv => srv.ServiceType == typeof(ApplicationPartManager));
            if (service == null || service.ImplementationInstance == null)
                throw new System.Exception($"尚未注册{nameof(ApplicationPartManager)}");

            var applicationPartMgr = service.ImplementationInstance as ApplicationPartManager;
            applicationPartMgr.FeatureProviders.Add(new RestControllerFeatureProvider());

            services.Configure<MvcOptions>(opt => opt.Conventions.Add(new RestControllerConvertion()));
            return services;
        }

        public static IServiceCollection AddApskJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = new JwtSetting();
            configuration.GetSection(nameof(JwtSetting)).Bind(jwtSetting);

            if (jwtSetting is null)
                throw new System.ArgumentNullException(nameof(jwtSetting));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.ClaimsIssuer = jwtSetting.Issuer;
                opt.Audience = jwtSetting.Audience;
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),
                    ValidAudience = jwtSetting.Audience,
                    ValidIssuer = jwtSetting.Issuer
                };
            });
            return services;
        }

        public static IServiceCollection AddApskOpenApiDocument(this IServiceCollection services, IConfiguration configuration)
        {
            var apiSetting = new OpenApiSetting();
            configuration.GetSection(nameof(OpenApiSetting)).Bind(apiSetting);

            if (apiSetting is null)
                throw new System.ArgumentNullException(nameof(apiSetting));

            if (string.IsNullOrWhiteSpace(apiSetting.Title))
                apiSetting.Title = Assembly.GetEntryAssembly().GetName().Name;

            services.AddOpenApiDocument(opt =>
            {
                opt.DocumentName = $"{apiSetting.Title}:{apiSetting.Version}";
                opt.Title = apiSetting.Title;
                opt.Version = apiSetting.Version;
                opt.Description = apiSetting.Description;
            });
            return services;
        }
    }
}