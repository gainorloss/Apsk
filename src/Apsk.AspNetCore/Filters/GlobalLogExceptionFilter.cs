using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apsk.AspNetCore.Filters
{
    public class GlobalLogExceptionFilter
        : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalLogExceptionFilter> _logger;
        public GlobalLogExceptionFilter(ILogger<GlobalLogExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var excep = filterContext.Exception;
                var controllerName = filterContext.RouteData.Values["controller"];
                var actionName = filterContext.RouteData.Values["action"];
                string tempMsg = $"在请求controller[{controllerName}] 的 action[{actionName}] 时产生异常【{excep.Message}】";
                if (excep.InnerException != null)
                {
                    tempMsg = $"{tempMsg} ,内部异常【{excep.InnerException.Message}】。";
                }
                if (excep.StackTrace != null)
                {
                    tempMsg = $"{tempMsg} ,异常堆栈【{excep.StackTrace}】。";
                }
                _logger.LogError(tempMsg);//Log.

                if (_env.IsDevelopment())
                    filterContext.Result = new JsonResult(RestResult.Error("500", $"接口请求异常，请联系管理员:{tempMsg}"));//In development,output exception message.
                else
                    filterContext.Result = new JsonResult(RestResult.Error("500", "接口请求异常，请联系管理员"));
                filterContext.ExceptionHandled = true;//Tag it is handled.
            }
        }
    }
}
