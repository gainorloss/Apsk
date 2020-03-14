// <copyright file="RestResult.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore
{
    /// <summary>
    /// Restful api return result.
    /// </summary>
    public class RestResult
    {
        private RestResult(bool success, string errorCode = "", string errorMsg = "", object result = null)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;

            if (result != null)
                Result = result;
        }

        private RestResult()
        {}

        /// <summary>
        /// Gets or sets success.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string ErrorMsg { get; private set; }

        /// <summary>
        /// Gets or sets result.
        /// </summary>
        public object Result { get; private set; }

        public static RestResult Create(bool success, string errorCode = "", string errorMsg = "", object result = null)
        {
            return new RestResult(success, errorCode, errorMsg, result);
        }

        public static RestResult Ok(object result = null)
        {
            return Create(true, result: result);
        }

        public static RestResult Error(string errorCode = "", string errorMsg = "")
        {
            return Create(false, errorCode, errorMsg);
        }
    }
}
