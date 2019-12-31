// <copyright file="AuthenticationResult.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Authentication
{
    using Newtonsoft.Json;

    public class AuthenticationToken
    {
        public AuthenticationToken(string access_token, int expires_in, string token_type)
        {
            AccessToken = access_token;
            ExpiresIn = expires_in;
            TokenType = token_type;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; private set; }

        [JsonProperty("token_type")]
        public string TokenType { get; private set; }
    }
}
