﻿// <copyright file="AbstractSendSmsRequest.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// 请求基类.
    /// </summary>
    public abstract class AbstractSendSmsRequest
    {
        public AbstractSendSmsRequest() => SetTemplateCode();

        public string AccessKeyId { get; } = "";
        public string Secret { get; } = "";
        [JsonIgnore]
        public string TemplateCode { get; protected set; }
        [JsonIgnore]
        public string Action { get; } = "SendSms";
        [JsonIgnore]
        public string SignName { get; } = "凤凰电商";
        public string Domain { get; } = "dysmsapi.aliyuncs.com";
        public string Version { get; } = "2017-05-25";

        /// <summary>
        /// 设置模板码（通过继承的方式设置各自场景下的模板码，传参的时候忽略这个值）.
        /// </summary>
        protected abstract void SetTemplateCode();
    }
}
