using Microsoft.AspNetCore.Mvc;

namespace Apsk.AspNetCore
{
    public class RestController : ControllerBase
    {
        [NonAction]
        public RestResult Success()
        {
            return RestResult.Ok();
        }

        [NonAction]
        public RestResult Success(object result = null)
        {
            return RestResult.Ok(result);
        }

        [NonAction]
        public RestResult Failure()
        {
            return RestResult.Error();
        }

        [NonAction]
        public RestResult Failure(string errorCode = "", string errorMsg = "")
        {
            return RestResult.Error(errorCode,errorMsg);
        }
    }
}
