// <copyright file="SendDispatchReportRequest.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// 发送调度通报请求
    /// </summary>
    public class SendDispatchReportRequest
        : AbstractSendSmsRequest
    {
        public SendDispatchReportRequest()
        {}

        public SendDispatchReportRequest(DateTime dispatcherTime,
            string dispatchNo,
            int batchCount,
            int orderCount)
        {
            DispatcherTime = dispatcherTime.ToShortTimeString();
            DispatchNo = dispatchNo;
            BatchCount = batchCount;
            OrderCount = orderCount;
        }

        [JsonProperty("dispatcher_time")]
        public string DispatcherTime { get; set; }

        [JsonProperty("dispatch_no")]
        public string DispatchNo { get; set; }

        [JsonProperty("batch_count")]
        public int BatchCount { get; set; }

        [JsonProperty("order_count")]
        public int OrderCount { get; set; }

        protected override void SetTemplateCode() => TemplateCode = "SMS_173475676";
    }
}
