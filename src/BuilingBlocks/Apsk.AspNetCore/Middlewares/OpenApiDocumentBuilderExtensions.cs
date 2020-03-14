// <copyright file="SwaggerUIBuilderExtensions.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Middlewares
{
    using Apsk.AspNetCore.AppSettings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using System.Reflection;

    public static class OpenApiDocumentBuilderExtensions
    {
        public static void UseApskOpenApiDocument(this IApplicationBuilder app, IConfiguration config)
        {
            var apiSetting = new OpenApiSetting();
            config.GetSection(nameof(OpenApiSetting)).Bind(apiSetting);

            if (apiSetting is null)
                throw new System.ArgumentNullException(nameof(apiSetting));

            if (string.IsNullOrWhiteSpace(apiSetting.Title))
                apiSetting.Title = Assembly.GetEntryAssembly().GetName().Name;

            app.UseOpenApi(setting =>
            {
                setting.DocumentName = $"{apiSetting.Title}:{apiSetting.Version}";
            });
            app.UseSwaggerUi3();
            app.UseReDoc();
        }
    }
}
