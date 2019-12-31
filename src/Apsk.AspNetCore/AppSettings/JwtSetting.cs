// <copyright file="JwtSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.AppSettings
{
    using Apsk.Annotations;

    /// <summary>
    /// Json web token setting.
    /// </summary>
    [PropertySource]
    public class JwtSetting
    {
        /// <summary>
        /// Gets or sets issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets secret.
        /// </summary>
        public string Secret { get; set; }
    }
}
