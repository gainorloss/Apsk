// <copyright file="SwaggerUIBuilderExtensions.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Middlewares
{
    using Apsk.AspNetCore.AppSettings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public static class OpenApiDocumentBuilderExtensions
    {
        public static void UseApskOpenApiDocument(this IApplicationBuilder app, IConfiguration config)
        {
            var apiSetting = new OpenApiSetting();
            config.GetSection(nameof(OpenApiSetting)).Bind(apiSetting);

            if (apiSetting is null)
                throw new System.ArgumentNullException(nameof(apiSetting));

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc();
        }
    }
}
