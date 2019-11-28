using System;

namespace Infrastructure.Web.Annotations
{
    /// <summary>
    /// @RestController 用户动态生成ApiController.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RestControllerAttribute
        : Attribute
    {
        /// <summary>
        /// 场景
        /// </summary>
        public string Scene { get; set; } = "api";

        /// <summary>
        /// 分隔符
        /// </summary>
        public string Separator { get; set; } = "/";

        /// <summary>
        /// 控制器后缀
        /// </summary>
        public string[] ControllerPostfixes { get; set; } = new string[] { "AppService", "ApplicationService" };

        /// <summary>
        /// 方法后缀
        /// </summary>
        public string[] ActionPostfixes { get; set; } = new string[] { "Async" };
    }
}