// <copyright file="RedisConnectionSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AppSettings
{
    using Apsk.Annotations;

    [PropertySource(IgnoreResourceNotFound =true)]
    public class RedisConnectionSetting
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
