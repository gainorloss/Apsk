// <copyright file="SendCaptchaRequest.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// 发送验证码请求.
    /// </summary>
    public class SendCaptchaRequest
        : AbstractSendSmsRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendCaptchaRequest"/> class.
        /// </summary>
        public SendCaptchaRequest()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SendCaptchaRequest"/> class.
        /// </summary>
        /// <param name="code"></param>
        public SendCaptchaRequest(string code)
        {
            Code = code;
        }

        /// <summary>
        /// 验证码.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        protected override void SetTemplateCode() => TemplateCode = "SMS_159620484";
    }
}
