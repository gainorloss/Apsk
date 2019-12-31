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
        public RestControllerAttribute(string scene)
        {
            Scene = scene;
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
        public string[] ControllerPostfixes { get; set; } = new string[] { "AppService", "ApplicationService", "AppSrv", "AppSvc" };

        /// <summary>
        /// 方法后缀.
        /// </summary>
        public string[] ActionPostfixes { get; set; } = new string[] { "Async" };
    }
}