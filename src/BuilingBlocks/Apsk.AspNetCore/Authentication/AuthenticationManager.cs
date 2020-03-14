// <copyright file="AuthenticationManager.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Apsk.Annotations;
    using Apsk.AspNetCore.AppSettings;
    using IdentityModel;
    using Microsoft.IdentityModel.Tokens;

    [Component]
    public class AuthenticationManager
        : IAuthenticationManager
    {
        private readonly JwtSetting _jwtSetting;

        public AuthenticationManager(JwtSetting jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }

        /// <inheritdoc/>
        public AuthenticationToken Authenticate(string appKey, string appSecret)
        {
            var claims = new List<Claim>()
            {
                  new Claim(JwtClaimTypes.Audience, _jwtSetting.Audience),
                  new Claim(JwtClaimTypes.Id, appKey),
                  new Claim(JwtClaimTypes.Issuer, _jwtSetting.Issuer),
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)), SecurityAlgorithms.HmacSha256Signature);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(_jwtSetting.Issuer, _jwtSetting.Audience, claims, DateTime.Now, DateTime.Now.AddHours(1), signingCredentials));
            return new AuthenticationToken(jwtToken, 3600, "Bearer");
        }
    }
}
