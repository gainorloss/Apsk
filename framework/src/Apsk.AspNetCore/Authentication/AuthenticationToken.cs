// <copyright file="AuthenticationResult.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Authentication
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Newtonsoft.Json;
    using System;

    public class AuthenticationToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationToken"/> class.
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="expires_in"></param>
        public AuthenticationToken(string access_token, int expires_in)
        {
            AccessToken = access_token;
            ExpiresIn = expires_in;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationToken"/> class.
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="expires_in"></param>
        /// <param name="token_type"></param>
        public AuthenticationToken(string access_token, int expires_in, string token_type)
            : this(access_token, expires_in)
        {
            TokenType = token_type;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; private set; }

        [JsonProperty("token_type")]
        public string TokenType { get; private set; } = JwtBearerDefaults.AuthenticationScheme;
    }
}
