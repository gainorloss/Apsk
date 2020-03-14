// <copyright file="SendSmsResponse.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Models
{
    /// <summary>
    /// 短信发送通用返回
    /// </summary>
    public class SendSmsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendSmsResponse"/> class.
        /// </summary>
        public SendSmsResponse()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendSmsResponse"/> class.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public SendSmsResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Code==0表示成功，1：失败.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 调试信息.
        /// </summary>
        public string Message { get; set; }
    }
}
