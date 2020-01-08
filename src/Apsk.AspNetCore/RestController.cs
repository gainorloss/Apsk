// <copyright file="RestController.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

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
            return RestResult.Error(errorCode, errorMsg);
        }
    }
}
