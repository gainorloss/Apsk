// <copyright file="SwaggerUIBuilderExtensions.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Middlewares
{
    using Apsk.AspNetCore.AppSettings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public static class SwaggerUIBuilderExtensions
    {
        public static void UseApskSwagger(this IApplicationBuilder app, IConfiguration config)
        {
            var apiSetting = new OpenApiSetting();
            config.GetSection(nameof(OpenApiSetting)).Bind(apiSetting);

            if (apiSetting is null)
                throw new System.ArgumentNullException(nameof(apiSetting));

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{apiSetting.Version}/swagger.json", apiSetting.Title));
        }
    }
}
