// <copyright file="RestControllerAttribute.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.Annotations
{
    using System;

    /// <summary>
    /// @RestController 用户动态生成ApiController.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RestControllerAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestControllerAttribute"/> class.
        /// </summary>
        public RestControllerAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestControllerAttribute"/> class.
        /// </summary>
        /// <param name="scene"></param>
        public RestControllerAttribute(string scene)
        {
            Scene = scene;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestControllerAttribute"/> class.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="version"></param>
        public RestControllerAttribute(string scene, string version)
            : this(scene)
        {
            Version = version;
        }

        /// <summary>
        /// 场景.
        /// </summary>
        public string Scene { get; set; } = "api";

        /// <summary>
        /// 分隔符.
        /// </summary>
        public string Separator { get; set; } = ".";

        /// <summary>
        /// 控制器后缀.
        /// </summary>
        public string[] ControllerPostfixes { get; set; } = new[] { "AppService", "AppSrv", "AppSvc", "ApplicationService", "ApplicationSrv", "ApplicationSvc" };

        /// <summary>
        /// 方法后缀.
        /// </summary>
        public string[] ActionPostfixes { get; set; } = new[] { "Async" };

        /// <summary>
        /// Gets or sets version.
        /// </summary>
        public string Version { get; set; } = "v1.0";
    }
}